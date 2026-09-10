namespace HelloBinX_CRUD_Operations.Dtos;

public class CreateBookDto
{
    public string BookName { get; set; } = string.Empty; // not empty, not null, All chars is letter

    public int AuthorId { get; set; }

    public int PublishedYear { get; set; }

    public decimal Price { get; set; }
}