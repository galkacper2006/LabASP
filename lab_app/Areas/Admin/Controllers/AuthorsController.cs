using lab_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_app.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorsController : Controller
{
    private static readonly List<Author> Authors = new()
    {
        new Author { Id = 1, Name = "Stanisław Lem" },
        new Author { Id = 2, Name = "Bolesław Prus" },
        new Author { Id = 3, Name = "Andrzej Sapkowski" }
    };

    public IActionResult Index()
    {
        return View(Authors.ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Author());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid)
            return View(author);

        author.Id = Authors.Max(a => a.Id) + 1;
        Authors.Add(author);

        TempData["Success"] = "Dodano autora!";
        return RedirectToAction(nameof(Index));
    }
}