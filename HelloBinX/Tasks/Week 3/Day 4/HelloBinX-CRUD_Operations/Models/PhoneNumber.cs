namespace HelloBinX_CRUD_Operations.Models;

public class PhoneNumber
{
    public int PhoneNumberId { get; set; }

    public int AuthorId { get; set; }

    public string Phone { get; set; } = string.Empty;

    // Navigation Property
    public Author Author { get; set; } = null!;
}