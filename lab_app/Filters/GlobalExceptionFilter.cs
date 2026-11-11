using lab_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace lab_app.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _log;

    public GlobalExceptionFilter(
        ILogger<GlobalExceptionFilter> log)
    {
        _log = log;
    }

    public void OnException(ExceptionContext context)
    {
        _log.LogError(context.Exception,
            "Błąd podczas obsługi {Path}",
            context.HttpContext.Request.Path);

        if (context.HttpContext.Request.Path
            .StartsWithSegments("/api"))
        {
            context.Result = new ObjectResult(
                new ProblemDetails
                {
                    Status = 500,
                    Title = "Wewnętrzny błąd serwera"
                })
            {
                StatusCode = 500
            };
        }
        else
        {
            context.Result = new ViewResult
            {
                ViewName = "Error",
                StatusCode = 500,
                ViewData = new ViewDataDictionary<ErrorViewModel>(
                    new EmptyModelMetadataProvider(),
                    context.ModelState)
                {
                    Model = new ErrorViewModel
                    {
                        RequestId =
                            context.HttpContext.TraceIdentifier
                    }
                }
            };
        }

        context.ExceptionHandled = true;
    }
}