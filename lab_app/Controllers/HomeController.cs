
using System.Diagnostics;
using lab_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

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
        [FromServices] IActionDescriptorCollectionProvider provider,
        [FromServices] EndpointDataSource dataSource)
    {
        var actionIds = provider.ActionDescriptors.Items
            .OfType<ControllerActionDescriptor>()
            .Select(a => a.Id)
            .ToHashSet();

        var routes = dataSource.Endpoints
            .OfType<RouteEndpoint>()
            .Select(e => new
            {
                Endpoint = e,
                Action = e.Metadata.GetMetadata<ControllerActionDescriptor>()
            })
            .Where(x => x.Action != null && actionIds.Contains(x.Action.Id))
            .Select(x =>
            {
                var action = x.Action!;

                action.RouteValues.TryGetValue("area", out var area);

                var pattern = x.Endpoint.RoutePattern.RawText ?? "";

                pattern = pattern
                    .Replace("{area:exists}", area ?? "")
                    .Replace("{controller=Home}", action.ControllerName)
                    .Replace("{controller=Dashboard}", action.ControllerName)
                    .Replace("{action=Index}", action.ActionName)
                    .Replace("{controller}", action.ControllerName)
                    .Replace("{action}", action.ActionName);

                var methods = x.Endpoint.Metadata
                    .GetMetadata<IHttpMethodMetadata>()?.HttpMethods;

                return new RouteInfo
                {
                    Pattern = "/" + pattern.TrimStart('/'),
                    Controller = action.ControllerName,
                    Action = action.ActionName,
                    Area = area,
                    Methods = methods is { Count: > 0 }
                        ? string.Join(", ", methods)
                        : "*"
                };
            })
            .DistinctBy(r => new
            {
                r.Pattern,
                r.Controller,
                r.Action,
                r.Methods
            })
            .OrderBy(r => r.Pattern)
            .ToList();

        return View(routes);
    }
    public IActionResult Boom()
    {
        throw new InvalidOperationException("Celowy wyjątek");
    }
    
    public IActionResult HttpStatus(int code = 404)
    {
        Response.StatusCode = code;
        return View(code);
    }


}
