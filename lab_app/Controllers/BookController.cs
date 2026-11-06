using System.Text;
using lab_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_app.Controllers;

public class BookController : Controller
{
    public IActionResult Index() => View(BookStoreMemory.Books);

    [HttpGet]
    public IActionResult Create() => View(new BookFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        BookStoreMemory.Books.Add(new Book
        {
            Id = BookStoreMemory.Books.Max(b => b.Id) + 1,
            Title = vm.Title,
            Isbn = vm.Isbn,
            PublicationDate = vm.PublicationDate,
            NumPages = vm.NumPages,
            Price = vm.Price,
            Author = vm.Author
        });

        TempData["Success"] = $"Dodano książkę „{vm.Title}”.";
        return RedirectToAction(nameof(Index));
    }

   
    public IActionResult Hello() =>
        Content("Witaj w BookStore!", "text/plain; charset=utf-8");

    public IActionResult Status(int code = 200) => StatusCode(code);

    public IActionResult Details(int id)
    {
        ViewBag.Id = id;
        return View();
    }

    public IActionResult Echo([FromQuery] string name = "nieznajomy", [FromQuery] int count = 1) =>
        Content(string.Join("\n", Enumerable.Repeat($"Cześć, {name}!", Math.Clamp(count, 1, 20))));

    public IActionResult Square(int id) => Content($"{id}^2 = {id * id}");

    [HttpPost]
    public IActionResult Import([FromBody] BookDto dto) =>
        Json(new { received = dto.Title, dto.Isbn, dto.Price });

    public IActionResult Inspect()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Method: {Request.Method}");
        sb.AppendLine($"Path:   {Request.Path}{Request.QueryString}");
        foreach (var h in Request.Headers)
            sb.AppendLine($"{h.Key}: {h.Value}");

        return Content(sb.ToString(), "text/plain; charset=utf-8");
    }

    public IActionResult Sum([FromQuery] int a, [FromQuery] int b)
    {
        if (!ModelState.IsValid)
            return BadRequest("Parametry a i b muszą być liczbami całkowitymi.");

        return Content((a + b).ToString());
    }

    public IActionResult UserAgent() =>
        Content(Request.Headers.UserAgent.ToString(), "text/plain; charset=utf-8");

    public IActionResult Health() =>
        Json(new { status = "ok", time = DateTime.Now });
}
