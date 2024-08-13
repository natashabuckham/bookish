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
    // public IActionResult BookList()
    // {
    //     var context = new BookishContext();
    //     BookshelfModel allBooks = new BookshelfModel();

    //     allBooks.Books = (from book in context.Books select new BookModel()
    //     {
    //         Id=book.Id,
    //         Name=book.Name,
    //         Author=book.Author,
    //         Genre=book.Genre
    //     }).ToList();
        
    //     return View(allBooks);
    // }

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
