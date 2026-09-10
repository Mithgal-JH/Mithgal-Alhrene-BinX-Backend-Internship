using HelloBinX_CRUD_Operations.Models;
using HelloBinX_CRUD_Operations.Data;
using Microsoft.EntityFrameworkCore;
using HelloBinX_CRUD_Operations.Dtos;
using HelloBinX_CRUD_Operations.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }


    // Retrieve all books from the database.
    public async Task<List<Book>> GetBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }


    // Retrieve a single book by its ID.
    public async Task<Book?> GetBookAsync(int id)
    {
        return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);
    }


    // Create a new book and save it to the database.
    public async Task<Book> CreateBookAsync(CreateBookDto dto)
    {
        Book book = new Book
        {
            BookName = dto.BookName,
            AuthorId = dto.AuthorId,
            PublishedYear = dto.PublishedYear,
            Price = dto.Price
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return book;
    }


    // Update the existing book with the provided data.
    public async Task<Book?> UpdateBookAsync(int id, UpdateBookDto dto)
    {
        Book? book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);

        if (book == null)
        {
            return null;
        }

        _context.Entry(book).CurrentValues.SetValues(dto);
        await _context.SaveChangesAsync();
        return book;
    }



    // Delete a book if it exists.
    public async Task<bool> DeleteBookAsync(int id)
    {
        Book? book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);
        
        if (book == null)
        {
            return false;
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}