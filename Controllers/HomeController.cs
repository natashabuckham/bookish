using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult BookList()
    {
        BookListModel allBooks = new BookListModel();
        BookModel prideAndPrejudice = new BookModel { Id = 1, Name = "Pride and Prejudice", Author = "Jane Austen", Genre = "Classic" };
        BookModel macbeth = new BookModel { Id = 2, Name = "Macbeth", Author = "Shakespeare", Genre = "Play" };
        allBooks.Books.Add(prideAndPrejudice);
        allBooks.Books.Add(macbeth);

        return View(allBooks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
