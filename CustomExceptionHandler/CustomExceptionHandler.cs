using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.CustomExceptionHandler;

public class CustomExceptionHandler(IProblemDetailsService problemDetails, ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is OperationCanceledException)
        {
            logger.LogWarning(exception, "Operation was cancelled");
            return true;  
        }

        if(exception is DomainException dex)
        {
            logger.LogWarning(exception, "Domain exception occurred");
            httpContext.Response.StatusCode = dex.StatusCode;
            return await problemDetails.TryWriteAsync(CreateCustomContext(dex, httpContext));
        }

        logger.LogWarning(exception.Message);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return await problemDetails.TryWriteAsync(CreateGenericContext(exception, httpContext));
    }

    public ProblemDetailsContext CreateCustomContext(DomainException exception, HttpContext httpContext)
    {
        return new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = exception.Title,
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }
        };
    }
    public ProblemDetailsContext CreateGenericContext(Exception exception, HttpContext httpContext)
    {
        return new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An unexpected error occured",
                Detail = "The server encountered an internal issue and could not complete your request. Please contact support if this continues.",
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }
        };
    }
}