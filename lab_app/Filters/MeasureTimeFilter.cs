using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lab_app.Filters;

public class MeasureTimeFilter : IAsyncActionFilter
{
    private readonly ILogger<MeasureTimeFilter> _log;

    public MeasureTimeFilter(ILogger<MeasureTimeFilter> log)
    {
        _log = log;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();

        await next();

        sw.Stop();

        context.HttpContext.Response.Headers["X-Action-Ms"] =
            sw.ElapsedMilliseconds.ToString();

        _log.LogInformation(
            "CZAS AKCJI: {Ms} ms",
            sw.ElapsedMilliseconds);
    }
}