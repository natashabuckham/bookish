namespace Bookish.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
}

// public class BookshelfModel
// {
//     public BookshelfModel()
//     {
//         Books = new List<BookModel>();
//     }
//     public int BookshelfId { get; set; }
//     public IList<BookModel> Books { get; set; }
// }