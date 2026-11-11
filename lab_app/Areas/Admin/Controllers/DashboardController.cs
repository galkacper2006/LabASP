using lab_app.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace lab_app.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly RequestStats _stats;

    public DashboardController(RequestStats stats)
    {
        _stats = stats;
    }

    public IActionResult Index()
    {
        ViewBag.TopPaths = _stats.TopPaths(5);
        return View();
    }
}