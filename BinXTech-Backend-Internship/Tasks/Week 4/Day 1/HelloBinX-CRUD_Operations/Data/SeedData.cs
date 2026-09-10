using HelloBinX_CRUD_Operations.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloBinX_CRUD_Operations.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // ==========================
        // Authors
        // ==========================
        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                AuthorId = 1,
                AuthorName = "Robert Martin",
                Bio = "Software Engineer and Author"
            },
            new Author
            {
                AuthorId = 2,
                AuthorName = "Martin Fowler",
                Bio = "Software Architect"
            }
        );

        // ==========================
        // Books
        // ==========================
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                BookId = 1,
                BookName = "Clean Code",
                AuthorId = 1,
                PublishedYear = 2008,
                Price = 50m
            },
            new Book
            {
                BookId = 2,
                BookName = "Refactoring",
                AuthorId = 2,
                PublishedYear = 2018,
                Price = 70m
            }
        );

        // ==========================
        // Phone Numbers
        // ==========================
        modelBuilder.Entity<PhoneNumber>().HasData(
            new PhoneNumber
            {
                PhoneNumberId = 1,
                AuthorId = 1,
                Phone = "0599000001"
            },
            new PhoneNumber
            {
                PhoneNumberId = 2,
                AuthorId = 2,
                Phone = "0599000002"
            }
        );

        // ==========================
        // Reviews
        // ==========================
        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                ReviewId = 1,
                BookId = 1,
                Rating = 5,
                Comment = "Excellent book",
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new Review
            {
                ReviewId = 2,
                BookId = 2,
                Rating = 4,
                Comment = "Very useful",
                CreatedAt = new DateTime(2026, 1, 2)
            }
        );
    }
}