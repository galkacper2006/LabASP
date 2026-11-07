
using System.Diagnostics;
using lab_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace lab_app.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    public IActionResult Docs(string? path)
    {
        return Content($"path = {path}");
    }

    public IActionResult Routes(
        [FromServices] IActionDescriptorCollectionProvider provider)
    {
        var routes = provider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(r =>
            {
                var area = r.RouteValues.TryGetValue("area", out var a)
                    ? a : null;

                var pattern = r.AttributeRouteInfo?.Template ??
                    (string.IsNullOrEmpty(area)
                        ? $"/{r.ControllerName}/{r.ActionName}"
                        : $"/{area}/{r.ControllerName}/{r.ActionName}");

                if (!pattern.StartsWith("/"))
                    pattern = "/" + pattern;

                var methods = r.ActionConstraints?
                    .OfType<HttpMethodActionConstraint>()
                    .SelectMany(c => c.HttpMethods)
                    .Distinct()
                    .ToArray();

                return new RouteInfo
                {
                    Pattern = pattern,
                    Controller = r.ControllerName,
                    Action = r.ActionName,
                    Area = area,
                    Methods = methods is { Length: > 0 }
                        ? string.Join(", ", methods)
                        : "*"
                };
            })
            .OrderBy(r => r.Pattern)
            .ToList();

        return View(routes);
    }
}
