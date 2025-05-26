using NetWebApiTemplate.Api.Models;
using NewWebApiTemplate.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace NetWebApiTemplate.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new ApiResponse<string>("Internal server error");

                if (ex is UserFriendlyException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                }
                else
                {
                    if (ex == AppExceptionFactory.Forbidden)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        response.Message = ex.Message;
                    }
                }

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
