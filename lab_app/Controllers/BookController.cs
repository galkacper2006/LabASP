using System.Text;
using lab_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_app.Controllers;

public class BookController : Controller
{
    // GET /Book/Hello
    public IActionResult Hello() =>
        Content("Witaj w BookStore!", "text/plain; charset=utf-8");

    // GET /Book/Status?code=418
    public IActionResult Status(int code = 200) => StatusCode(code);

    // GET /Book/Details/3
    public IActionResult Details(int id)
    {
        ViewBag.Id = id;
        return View();
    }

    // GET /Book/Echo?name=Ala&count=2
    public IActionResult Echo([FromQuery] string name = "nieznajomy", [FromQuery] int count = 1) =>
        Content(string.Join("\n", Enumerable.Repeat($"Cześć, {name}!", Math.Clamp(count, 1, 20))));

    // GET /Book/Square/7
    public IActionResult Square(int id) => Content($"{id}^2 = {id * id}");

    // POST /Book/Import   Content-Type: application/json
    [HttpPost]
    public IActionResult Import([FromBody] BookDto dto) =>
        Json(new { received = dto.Title, dto.Isbn, dto.Price });

    // GET /Book/Inspect
    public IActionResult Inspect()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Method: {Request.Method}");
        sb.AppendLine($"Path:   {Request.Path}{Request.QueryString}");
        foreach (var h in Request.Headers)
            sb.AppendLine($"{h.Key}: {h.Value}");

        return Content(sb.ToString(), "text/plain; charset=utf-8");
    }

    // GET /Book/Sum?a=2&b=3
    public IActionResult Sum([FromQuery] int a, [FromQuery] int b)
    {
       
        if (!ModelState.IsValid)
            return BadRequest("Parametry a i b muszą być liczbami całkowitymi.");

        return Content((a + b).ToString());
    }

    // GET /Book/UserAgent
    public IActionResult UserAgent() =>
        Content(Request.Headers.UserAgent.ToString(), "text/plain; charset=utf-8");

    // GET /Book/Health
    public IActionResult Health() =>
        Json(new { status = "ok", time = DateTime.Now });
}
