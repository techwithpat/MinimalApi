using Microsoft.EntityFrameworkCore;

namespace MinimalApi
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options):base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }

    public class Book
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
