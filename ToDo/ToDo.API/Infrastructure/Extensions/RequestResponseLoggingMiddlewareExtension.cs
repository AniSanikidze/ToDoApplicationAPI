using ToDoApp.API.Infrastructure.Middlewares;

namespace ToDoApp.API.Infrastructure.Extensions
{
    public static class RequestResponseLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseReqResLogging(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<RequestLoggingMiddleware>();
            return applicationBuilder;
        }
    }
}
