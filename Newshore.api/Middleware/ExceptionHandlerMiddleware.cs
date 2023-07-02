using System;
using System.Net;
using System.Text.Json;
using Error = Newshore.api.Model.Error;

namespace Newshore.api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            { 
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ExceptionHandlerMiddleware> logger)
        {
            var error = new Error(HttpStatusCode.InternalServerError, "Internal Server Error", ex);

            var json = JsonSerializer.Serialize(error);
            logger.LogError(new Exception(json), "Internal Server Error");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            return context.Response.WriteAsync(json);
        }
    }
}
