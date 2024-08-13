using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class CatalogueController : Controller
{
    private readonly ILogger<CatalogueController> _logger;

    public CatalogueController(ILogger<CatalogueController> logger)
    {
        _logger = logger;
    }

    public IActionResult BookList()
    {
         var context = new BookishContext();
        BookshelfModel allBooks = new BookshelfModel();

        allBooks.Books = (from book in context.Books select new BookModel()
        {
            Id=book.Id,
            Name=book.Name,
            Author=book.Author,
            Genre=book.Genre
        }).ToList();
        
        return View(allBooks);
    }
    
    [HttpPost]
    public ActionResult SearchResult()
    {
        return View("searchinput");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
