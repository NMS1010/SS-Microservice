using Microsoft.AspNetCore.Http;
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

        public ExceptionMiddleware(RequestDelegate next)
        { _next = next; }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = new CustomAPIResponse<NoContentAPIResponse>();
                response.StatusCode = error switch
                {
                    ForbiddenAccessException e => (int)HttpStatusCode.Forbidden,
                    NotFoundException e => (int)HttpStatusCode.NotFound,
                    SecurityTokenException e => (int)HttpStatusCode.BadRequest,
                    UnauthorizedException e => (int)HttpStatusCode.Unauthorized,
                    ValidationException e => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };
                response.IsSuccess = false;
                response.Errors = new List<string> { error.Message };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}