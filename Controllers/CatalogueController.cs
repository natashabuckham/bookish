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

    public IActionResult BookList(string sortOrder)
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
        ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
        BookshelfModel allBooks = new BookshelfModel();

        // allBooks.Books = (from book in _context.Books orderby book.Name select new BookModel()
        // {
        //     Id=book.Id,
        //     Name=book.Name,
        //     Author=book.Author,
        //     Genre=book.Genre
        // });

        switch (sortOrder)
        {
            // case "name_asc":
            //     allBooks.Books = allBooks.Books.OrderByAscending(book => book.Name);
            //     break;
            case "name_desc":
                allBooks.Books = (from book in _context.Books orderby book.Name descending select new BookModel()
                {
                Id=book.Id,
                Name=book.Name,
                Author=book.Author,
                Genre=book.Genre
                }).ToList();  
                break;
            case "Author":
                allBooks.Books = (from book in _context.Books orderby book.Author select new BookModel()
                {
                Id=book.Id,
                Name=book.Name,
                Author=book.Author,
                Genre=book.Genre
                }).ToList();  
                break;
             case "author_desc":
                allBooks.Books = (from book in _context.Books orderby book.Author descending select new BookModel()
                {
                Id=book.Id,
                Name=book.Name,
                Author=book.Author,
                Genre=book.Genre
                }).ToList();  
                break;
             case "Genre":
                allBooks.Books = (from book in _context.Books orderby book.Genre select new BookModel()
                {
                Id=book.Id,
                Name=book.Name,
                Author=book.Author,
                Genre=book.Genre
                }).ToList();  
                break;
             case "genre_desc":
                allBooks.Books = (from book in _context.Books orderby book.Genre descending select new BookModel()
                {
                Id=book.Id,
                Name=book.Name,
                Author=book.Author,
                Genre=book.Genre
                }).ToList();  
                break;
            default:
                allBooks.Books = (from book in _context.Books orderby book.Name select new BookModel()
                {
                Id=book.Id,
                Name=book.Name,
                Author=book.Author,
                Genre=book.Genre
                }).ToList();  
                break;
        }
        
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
