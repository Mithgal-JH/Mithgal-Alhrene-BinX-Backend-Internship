namespace HelloBinX_CRUD_Operations.Dtos;

public class UpdateBookDto
{
    public string BookName { get; set; } = string.Empty;

    public int AuthorId { get; set; }

    public int PublishedYear { get; set; }

    public decimal Price { get; set; }
}