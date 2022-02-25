using Microsoft.EntityFrameworkCore;

namespace library_api;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
        
    }
    
    public DbSet<Book?> Books { get; set; }
}