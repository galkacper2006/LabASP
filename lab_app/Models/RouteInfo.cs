using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace lab_app.Models;

public class RouteInfo
{
    public string Pattern{get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string? Area  { get; set; }
    public string Methods { get; set; }
}
public class HomeController : Controller
{   
public IActionResult Index()
{
    return View();
}
//
public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
{
    var routes = provider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>()
        .Select(r =>
            {
                return new RouteInfo()
                    {
                        Pattern = "/" + (r.AttributeRouteInfo?.Template ?? "Konwencjonalna"), //samodzielnie zbuduj łancuch z trasą konwencjonalną: <area>/<controller>/<action>
                        Action = r.ActionName,
                        Controller = r.ControllerName,
                        Methods = string.Join(",",
                            r.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                            .SelectMany(c => c.HttpMethods ?? ["*"]) ?? Array.Empty<string>()
                            ),
                        Area = r.RouteValues.TryGetValue("area", out var area) ? area : null
                        };
                    });
    return View(routes);
 }
}
 
 

                    
