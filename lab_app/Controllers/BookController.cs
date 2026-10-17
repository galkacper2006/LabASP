using lab_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_app.Controllers;

public class BookController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(BookStoreMemory.Books);
    }
}