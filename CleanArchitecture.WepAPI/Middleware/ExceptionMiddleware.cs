using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;
using System.Text.Json;

namespace CleanArchitecture.WepAPI.Middleware
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;
        public ExceptionMiddleware(AppDbContext context)
        {
        _context=context;
        }
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            await LogExceptionToDatabaseAsync(ex, context.Request);
            if (ex is ValidationException validationException)
            {
                context.Response.StatusCode = 400;
                var errors = validationException.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                });

                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Errors = errors }));
                return;
            }

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = ex.Message }));
        }
    
        private async Task LogExceptionToDatabaseAsync(Exception ex,HttpRequest request)
        {
            ErrorLog errorLog = new()
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                RequestPath = request.Path,
                RequestMethod = request.Method,
                Timestamp = DateTime.Now,

            };
            await _context.Set<ErrorLog>().AddAsync(errorLog,default);
            await _context.SaveChangesAsync();
        }
    }

}
