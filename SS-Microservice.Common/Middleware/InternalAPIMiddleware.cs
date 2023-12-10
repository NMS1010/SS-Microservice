using Microsoft.AspNetCore.Http;

namespace SS_Microservice.Common.Middleware
{
    public class InternalAPIMiddleware
    {
        private readonly RequestDelegate _next;

        public InternalAPIMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var isInternalAPI = request.Path.Value.Contains("internal");

            if (isInternalAPI)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            await _next(context);
        }
    }
}
