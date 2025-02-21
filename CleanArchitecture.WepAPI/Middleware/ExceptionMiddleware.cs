using FluentValidation;
using System.Text.Json;

namespace CleanArchitecture.WepAPI.Middleware
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            
            if (ex is ValidationException validationException)
            {
                context.Response.StatusCode = 400; 
                var errors = validationException.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                });

                return context.Response.WriteAsync(JsonSerializer.Serialize(new { Errors = errors }));
            }

            
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = ex.Message }));
        }
    }
}
