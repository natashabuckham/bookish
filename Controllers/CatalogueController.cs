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

    [HttpGet]
    public IActionResult BookList(string sortOrder)
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
        ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";

        List<BookModel> allBooks;

        switch (sortOrder)
        {
            case "name_desc":
               allBooks = _context.Books.OrderByDescending(book => book.Name).ToList(); 
                break;
            case "Author":
                allBooks = _context.Books.OrderBy(book => book.Author).ToList(); 
                break;
             case "author_desc":
                allBooks = _context.Books.OrderByDescending(book => book.Author).ToList();
                break;
             case "Genre":
                allBooks = _context.Books.OrderBy(book => book.Genre).ToList();
                break;
             case "genre_desc":
                allBooks = _context.Books.OrderByDescending(book => book.Genre).ToList(); 
                break;
            default:
                allBooks = _context.Books.OrderBy(book => book.Name).ToList(); 
                break;
        }
        
        return View(allBooks);
    }

    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BookModel foundBook = _context.Books.Find(id);

        if (foundBook == null)
        {
            return NotFound();
        }

        return View(foundBook);
    }

    [HttpPost]
    public ActionResult SearchResult(string searchInput)
    {
        BookModel foundBook = _context.Books.Where(book => book.Name == searchInput).FirstOrDefault<BookModel>();
        return View(foundBook);
    }

    [HttpGet]
    public IActionResult AddBook()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookViewModel viewModel)
    {
        var newBook = new BookModel
        {
            Name = viewModel.Name,
            Author = viewModel.Author,
            Genre = viewModel.Genre,
            Description = viewModel.Description,
            TotalCopies = viewModel.TotalCopies,
            AvailableCopies = viewModel.AvailableCopies
        };
        await _context.Books.AddAsync(newBook);

        await _context.SaveChangesAsync();

        return View();
    }
    
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
