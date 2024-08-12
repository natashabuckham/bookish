namespace Bookish.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
}

public class BookListModel
{
    public List<BookModel> Books { get; set; }
}