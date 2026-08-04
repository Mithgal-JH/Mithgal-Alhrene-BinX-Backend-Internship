namespace HelloBinX_EFCore.Models;

public class Author
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = string.Empty;

    public string? Bio { get; set; }

    // Navigation Properties
    public List<Book> Books { get; set; } = new();

    public List<PhoneNumber> PhoneNumbers { get; set; } = new();
}