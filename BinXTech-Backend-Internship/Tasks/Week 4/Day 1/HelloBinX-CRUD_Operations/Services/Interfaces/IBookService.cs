


using HelloBinX_CRUD_Operations.Dtos;
using HelloBinX_CRUD_Operations.Models;

namespace HelloBinX_CRUD_Operations.Services;

public interface IBookService
{
    /// <summary>
    /// Retrieves all books.
    /// </summary>
    Task<List<Book>> GetBooksAsync();

    /// <summary>
    /// Retrieves a book by its ID.
    /// </summary>
    Task<Book?> GetBookAsync(int id);

    /// <summary>
    /// Creates a new book.
    /// </summary>
    Task<Book> CreateBookAsync(CreateBookDto dto);

    /// <summary>
    /// Updates an existing book.
    /// </summary>
    Task<Book?> UpdateBookAsync(int id, UpdateBookDto dto);

    /// <summary>
    /// Deletes a book by its ID.
    /// </summary>
    Task<bool> DeleteBookAsync(int id);
}