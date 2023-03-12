using ToDoApp.API.Infrastructure.Middlewares;

namespace ToDoApp.API.Infrastructure.Extensions
{
    public static class GlobalHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
            return applicationBuilder;
        }
    }
}
