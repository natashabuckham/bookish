using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class CatalogueController : Controller
{
    private readonly ILogger<CatalogueController> _logger;

    private readonly Bookish.BookishContext _context;

    public CatalogueController(ILogger<CatalogueController> logger)
    {
        _logger = logger;
        _context = new BookishContext();
    }

    public IActionResult BookList()
    {
        BookshelfModel allBooks = new BookshelfModel();

        allBooks.Books = (from book in _context.Books select new BookModel()
        {
            Id=book.Id,
            Name=book.Name,
            Author=book.Author,
            Genre=book.Genre
        }).ToList();
        
        return View(allBooks);
    }
    
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BookshelfModel foundBooks = new BookshelfModel();

        foundBooks.Books = (from book in _context.Books where book.Id == id select new BookModel()
        {
            Id=book.Id,
            Name=book.Name,
            Author=book.Author,
            Genre=book.Genre
        }).ToList();
        
        // book = _context.Books.First(book => book.Id == id);

        if (foundBooks.Books == null)
        {
            return NotFound();
        }

        return View(foundBooks);
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
