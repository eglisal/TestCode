using Newtonsoft.Json;

namespace MedicareApi.Middlewares
{
    public class ErrorHandlingMdw
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMdw> _logger;

        public ErrorHandlingMdw(RequestDelegate next, ILogger<ErrorHandlingMdw> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled exception: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            Console.Write(context.Response);

            var result = JsonConvert.SerializeObject(new { error = $"HandleException. Internal Server Error: {exception.Message}" });
            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMdw>();
        }
    }
}
