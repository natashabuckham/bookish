using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish
{
    public class BookishContext : DbContext
    {
        public BookishContext(DbContextOptions<BookishContext> options): base(options)
        {
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<MemberModel> Members { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=bookish;User Id=bookish;Password=bookish;");
        // }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<BookModel>().Property(e => e.Id).UseIdentityAlwaysColumn();
        // }
    }
}