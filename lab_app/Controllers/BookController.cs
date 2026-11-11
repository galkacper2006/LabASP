
using System.Text;
using System.Linq;
using lab_app.Models;
using Microsoft.AspNetCore.Mvc;
using lab_app.Filters;

namespace lab_app.Controllers;
[ServiceFilter(typeof(LogActionFilter), Order = 1)]
[ServiceFilter(typeof(MeasureTimeFilter), Order = 2)]



public class BookController : Controller
{
    public IActionResult Index() => View(BookStoreMemory.Books);

    [HttpGet]
    public IActionResult Create() => View(new BookFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        BookStoreMemory.Books.Add(new Book
        {
            Id = BookStoreMemory.Books.Count == 0
                ? 1
                : BookStoreMemory.Books.Max(b => b.Id) + 1,
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

    [HttpGet("Book/Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var book = BookStoreMemory.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
            return NotFound();

        ViewBag.Id = id;
        return await Task.FromResult<IActionResult>(View());
    }

    public IActionResult Echo(
        [FromQuery] string name = "nieznajomy",
        [FromQuery] int count = 1) =>
        Content(string.Join("\n",
            Enumerable.Repeat($"Cześć, {name}!", Math.Clamp(count, 1, 20))));

    public IActionResult Square(int id) =>
        Content($"{id}^2 = {id * id}");

    [HttpPost]
    public IActionResult Import([FromBody] BookDto dto) =>
        Json(new { received = dto.Title, dto.Isbn, dto.Price });

    public IActionResult Inspect()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Method: {Request.Method}");
        sb.AppendLine($"Path: {Request.Path}{Request.QueryString}");

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
        Content(Request.Headers.UserAgent.ToString(),
            "text/plain; charset=utf-8");

    public IActionResult Health() =>
        Json(new { status = "ok", time = DateTime.Now });

    public async Task<IActionResult> ByYear(int year)
    {
        var books = BookStoreMemory.Books
            .Where(b => b.PublicationDate?.Year == year)
            .ToList();

        return await Task.FromResult<IActionResult>(
            View("Index", books));
    }

    public async Task<IActionResult> ByIsbn(string isbn)
    {
        var book = BookStoreMemory.Books.FirstOrDefault(b =>
            new string(b.Isbn.Where(char.IsDigit).ToArray()) == isbn);

        if (book == null)
            return NotFound();

        return await Task.FromResult<IActionResult>(
            RedirectToAction(nameof(Details), new { id = book.Id }));
    }
    
    public IActionResult Boom()
    {
        throw new InvalidOperationException("Celowy wyjątek");
    }
    
    public IActionResult Archive(int year, int month)
    {
        var books = BookStoreMemory.Books
            .Where(b =>
                b.PublicationDate?.Year == year &&
                b.PublicationDate?.Month == month)
            .ToList();

        return View("Index", books);
    }


}
