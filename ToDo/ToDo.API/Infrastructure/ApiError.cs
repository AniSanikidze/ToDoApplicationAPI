using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Net;
using ToDoApp.Application.CustomExceptions;

namespace ToDoApp.API.Infrastructure
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandledErrorCode = "UnhandledError";
        private HttpContext _context;
        private Exception _exception;

        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }
        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }
                return null;
            }
            set => Extensions["TraceId"] = value;
        }

        public ApiError(HttpContext context, Exception exception)
        {
            _context = context;
            _exception = exception;
            TraceId = context.TraceIdentifier;
            LogLevel = LogLevel.Error;
            Code = UnhandledErrorCode;
            Status = StatusCodes.Status500InternalServerError;
            Title = exception.Message;
            Instance = context.Request.Path;

            HandleException((dynamic)exception);
        }

        private void HandleException(ItemNotFoundException exception)
        {
            Code = exception.DomainClassName;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(ItemAlreadyExistsException exception)
        {
            Code = exception.DomainClassName;
            Status = (int)HttpStatusCode.Conflict;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(BadRequestWhenUpdatingWithPatch exception)
        {
            Code = "BadRequestOnPatch";
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(InvalidUserCredentialsException exception)
        {
            Code = "InvalidUserCredentials";
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(Exception exception)
        {

        }
    }
}
