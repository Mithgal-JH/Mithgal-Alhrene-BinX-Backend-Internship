namespace HelloBinX_EFCore.Models;

public class Review
{
    public int ReviewId { get; set; }

    public int BookId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation Property
    public Book Book { get; set; } = null!;
}