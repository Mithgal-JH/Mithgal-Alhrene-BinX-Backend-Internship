

using HelloBinX_CRUD_Operations.Dtos;
using HelloBinX_CRUD_Operations.Models;
using HelloBinX_CRUD_Operations.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // Returns all books.
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(await _bookService.GetBooksAsync());
    }


    // Returns a single book by ID.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        Book? book = await _bookService.GetBookAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }


    // Creates a new book.
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto dto)
    {
        var book = await _bookService.CreateBookAsync(dto);
        return CreatedAtAction(
                                nameof(GetBook),
                                new { id = book.BookId },
                                book
                              );
    }
    

    // Deletes a book by ID.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {

        if (id < 0)
        {
            return BadRequest();
        }

        bool deleted = await _bookService.DeleteBookAsync(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }


    // Updates an existing book.
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
    {
        Book? updatedBook = await _bookService.UpdateBookAsync(id, dto);

        if (updatedBook == null)
        {
            return NotFound();
        }
        return Ok(updatedBook);
    }

}