namespace Bookish.Models;

public class BookViewModel
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
}