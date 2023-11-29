using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SS_Microservice.Common.Swagger
{
    public static class SwaggerExtension
    {
        private static readonly string AUTH_TYPE = "Bearer";

        private static readonly OpenApiSecurityRequirement requirement = new()
        {{
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AUTH_TYPE
                },
                Scheme = "oauth2",
                Name = AUTH_TYPE,
                In = ParameterLocation.Header
            },
            Array.Empty<string>()
        }};

        private static readonly OpenApiSecurityScheme scheme = new()
        {
            Description = @"JWT authorization header using the Bearer sheme. \r\n\r\n
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = AUTH_TYPE
        };

        private static void AddJWTAuth(this SwaggerGenOptions option)
        {
            option.AddSecurityDefinition(AUTH_TYPE, scheme);
            option.OperationFilter<SecurityRequirementsOperationFilter>();
        }

        public static void AddSwaggerGenWithJWTAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt => opt.AddJWTAuth());
        }

        private class SecurityRequirementsOperationFilter : IOperationFilter
        {
            private static bool HasAttribute(MethodInfo methodInfo, Type type, bool inherit)
            {
                var actionAttributes = methodInfo.GetCustomAttributes(inherit);
                var controllerAttributes = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(inherit);
                var actionAndControllerAttributes = actionAttributes.Union(controllerAttributes);

                return actionAndControllerAttributes.Any(attr => attr.GetType() == type);
            }
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                bool hasAuthorizeAttribute = HasAttribute(context.MethodInfo, typeof(AuthorizeAttribute), true);
                bool hasAnonymousAttribute = HasAttribute(context.MethodInfo, typeof(AllowAnonymousAttribute), true);

                bool isAuthorized = hasAuthorizeAttribute && !hasAnonymousAttribute;
                if (isAuthorized)
                {
                    operation.Security = new List<OpenApiSecurityRequirement>
                    {
                        requirement
                    };
                }
            }
        }
    }
}
