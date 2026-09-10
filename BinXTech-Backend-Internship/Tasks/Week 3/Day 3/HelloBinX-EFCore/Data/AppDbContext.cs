using HelloBinX_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloBinX_EFCore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
}