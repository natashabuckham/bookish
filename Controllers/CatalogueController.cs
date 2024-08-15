using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class CatalogueController : Controller
{
    private readonly Bookish.BookishContext bookishContext;

    public CatalogueController (BookishContext bookishContext)
    {
        this.bookishContext = bookishContext;
    }

    public IActionResult BookList(string sortOrder, string searchString = "")
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
        ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";

        List<BookModel> allBooks;

        switch (sortOrder)
        {
            case "name_desc":
                allBooks = bookishContext.Books.OrderByDescending(book => book.Name).ToList();
                break;
            case "Author":
                allBooks = bookishContext.Books.OrderBy(book => book.Author).ToList();
                break;
            case "author_desc":
                allBooks = bookishContext.Books.OrderByDescending(book => book.Author).ToList();
                break;
            case "Genre":
                allBooks = bookishContext.Books.OrderBy(book => book.Genre).ToList();
                break;
            case "genre_desc":
                allBooks = bookishContext.Books.OrderByDescending(book => book.Genre).ToList();
                break;
            default:
                allBooks = bookishContext.Books.OrderBy(book => book.Name).ToList();
                break;
        }

        if (!String.IsNullOrEmpty(searchString))
        {
            allBooks = bookishContext.Books.Where(book => book.Name!.ToUpper().Contains(searchString.ToUpper())).ToList();
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

        BookModel foundBook = bookishContext.Books.Find(id);

        if (foundBook == null)
        {
            return NotFound();
        }

        return View(foundBook);
    }

    [HttpPost]
    public ActionResult SearchResult(string searchInput)
    {
        BookModel foundBook = bookishContext.Books.Where(book => book.Name == searchInput).FirstOrDefault<BookModel>();
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
        await bookishContext.Books.AddAsync(newBook);

        await bookishContext.SaveChangesAsync();

        return RedirectToAction("BookList", "Catalogue");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditBook(int id)
    {
        var book = await bookishContext.Books.FindAsync(id);

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(BookModel viewModel)
    {
        var book = await bookishContext.Books.FindAsync(viewModel.Id);
        if (book != null) {
            book.Name = viewModel.Name;
            book.Author = viewModel.Author;
            book.Genre = viewModel.Genre;
            book.Description = viewModel.Description;
            book.TotalCopies = viewModel.TotalCopies;
            book.AvailableCopies = viewModel.AvailableCopies;

            await bookishContext.SaveChangesAsync();
        }

        return RedirectToAction("BookList", "Catalogue");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(BookModel viewModel)
    {
        var book = await bookishContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

        if (book != null)
        {
            bookishContext.Books.Remove(viewModel);
            await bookishContext.SaveChangesAsync();
        }

        return RedirectToAction("BookList", "Catalogue");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
