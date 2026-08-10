namespace HelloBinX_CRUD_Operations.Models;

public class Book
{
    public int BookId { get; set; }

    public string BookName { get; set; } = string.Empty;

    public int AuthorId { get; set; }

    public int PublishedYear { get; set; }

    public decimal Price { get; set; }

    // Navigation Properties
    public Author Author { get; set; } = null!;

    public List<Review> Reviews { get; set; } = new();
}