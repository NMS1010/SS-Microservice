using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;
using SS_Microservice.Common.Types.Model.CustomResponse;
using System.Reflection;

namespace SS_Microservice.Common.Validators
{
    public static class Extension
    {
        [Obsolete]
        public static void ConfigureValidationErrorResponse(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var listViolation = new List<Violations>();
                    context.ModelState.Keys.ToList().ForEach(x =>
                    {
                        listViolation.Add(new Violations()
                        {
                            Field = x,
                            Messages = context.ModelState[x]?.Errors.Select(e => e.ErrorMessage).ToList()
                        });
                    });
                    return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new ErrorResponse()
                    {
                        Violations = listViolation,
                        Title = "One or more validation errors occurred",
                        TraceId = context.HttpContext.TraceIdentifier,
                        Instance = context.HttpContext.Request.Path,
                        Status = 400,
                        Type = new Uri("https://tools.ietf.org/html/rfc7231#section-6.5.1"),
                        Code = "VALIDATION_ERROR",
                    });
                };
            }).AddFluentValidation(v =>
            {
                v.ImplicitlyValidateChildProperties = true;
                v.ImplicitlyValidateRootCollectionElements = true;
                v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
        }

        public static void AddProblemDetailsSetup(this IServiceCollection services)
        {
            services.AddProblemDetails(setup =>
            {
                setup.IncludeExceptionDetails = (ctx, env) =>
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Staging";
            });
        }
    }
}