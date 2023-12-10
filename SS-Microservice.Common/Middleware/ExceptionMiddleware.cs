using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SS_Microservice.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace SS_Microservice.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next;
            _problemDetailsFactory = problemDetailsFactory;
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
                    AccessDeniedException => (int)HttpStatusCode.Forbidden,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    InvalidRequestException => (int)HttpStatusCode.BadRequest,
                    InternalServiceCommunicationException => (int)HttpStatusCode.InternalServerError,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var problemDetails = _problemDetailsFactory
                    .CreateProblemDetails(context, statusCode: statusCode, detail: error.Message, instance: context.Request.Path);

                string strJson = JsonSerializer.Serialize(problemDetails);
                context.Response.Headers.Add("Content-Type", "application/json");
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(strJson);
            }
        }
    }
}