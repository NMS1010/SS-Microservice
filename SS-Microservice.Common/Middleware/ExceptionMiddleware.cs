using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SS_Microservice.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next; _problemDetailsFactory = problemDetailsFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var statusCode = error switch
                {
                    ForbiddenAccessException => (int)HttpStatusCode.Forbidden,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var problemDetails = _problemDetailsFactory
                    .CreateProblemDetails(context, statusCode: statusCode, detail: error.Message, instance: context.Request.Path);

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}