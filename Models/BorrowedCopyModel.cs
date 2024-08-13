namespace Bookish.Models;

public class BorrowedCopyModel
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime DateTaken { get; set; }
    public DateTime DateDue { get; set; }
    public bool Overdue { get; set; }
}

public class AllBorrowedCopyModel
{
        public AllBorrowedCopyModel()
    {
        BorrowedBooks = new List<BorrowedCopyModel>();
    }
    public int AllBorrowedCopyId { get; set; }
    public IList<BorrowedCopyModel> BorrowedBooks { get; set; }
}