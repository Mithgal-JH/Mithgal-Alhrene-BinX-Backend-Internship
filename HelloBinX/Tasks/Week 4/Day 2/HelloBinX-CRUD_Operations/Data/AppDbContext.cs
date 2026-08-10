using HelloBinX_CRUD_Operations.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelloBinX_CRUD_Operations.Data;

public class AppDbContext : IdentityDbContext<IdentityUser> //IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    SeedData.Seed(modelBuilder);
}
}