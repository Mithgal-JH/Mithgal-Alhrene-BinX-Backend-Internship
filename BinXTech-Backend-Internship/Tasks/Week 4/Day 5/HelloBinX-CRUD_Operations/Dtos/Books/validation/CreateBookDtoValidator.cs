

using FluentValidation;
using HelloBinX_CRUD_Operations.Dtos;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{


    public CreateBookDtoValidator()
    {
        RuleFor(book => book.BookName)
        .NotEmpty()
            .WithMessage("{PropertyName} must be not empty")
        .Must(BeValidName)
            .WithMessage("{PropertyName} must be valid name\"All characters is letters\"")
        .Length(2, 150)
            .WithMessage("{PropertyName} must be between 2 and 150 characters");

        RuleFor(book => book.AuthorId)
        .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0");

        RuleFor(book => book.Price)
            .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal 0");

        RuleFor(book => book.PublishedYear)
        .InclusiveBetween(1000, DateTime.UtcNow.Year)
            .WithMessage("{PropertyName} must be between 1000 and the current year");
    }

    private bool BeValidName(string BookName)
    {
        if (!string.IsNullOrEmpty(BookName))
            return BookName.All(char.IsLetter);

        return false;
    }
}