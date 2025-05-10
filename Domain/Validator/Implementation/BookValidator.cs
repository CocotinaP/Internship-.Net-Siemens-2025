using System.Text;
using Internship.NetSiemens2025.domain.validator.util;
using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.domain.validator.concrete_validator;

/// <summary>
/// Validation class for book.
/// </summary>
public class BookValidator : AbstractValidator<Book>
{
    public override void Validate(Book entity)
    {
        var validationActions = new List<Action>
        {
            () => ValidateTitle(entity.Title),
            () => ValidateAuthor(entity.Author),
            () => ValidateQuantity(entity.Quantity),
        };
        
        RunValidation(validationActions);
    }

    /// <summary>
    /// Book title validation.
    /// </summary>
    /// <param name="title">
    /// Title to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the title is not valid.
    /// </exception>
    private void ValidateTitle(string title)
    {
        var errorMessage = new StringBuilder();

        if (title.Equals(string.Empty))
        {
            errorMessage.AppendLine("Title can not be empty!");
        }

        if (errorMessage.Length > 0)
        {
            throw new ValidationException(errorMessage.ToString());
        }
    }

    /// <summary>
    /// Book author validation.
    /// </summary>
    /// <param name="author">
    /// Author to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the author is not valid.
    /// </exception>
    private void ValidateAuthor(string author)
    {
        ValidatorUtils.ValidateName(author);
    }

    /// <summary>
    /// Book quantity validation.
    /// </summary>
    /// <param name="quantity">
    /// Quantity to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the quantity is not valid.
    /// </exception>
    private void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ValidationException("Quantity can not be less or equal to zero!");
        }
    }
}