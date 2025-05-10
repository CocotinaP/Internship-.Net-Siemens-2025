using System.ComponentModel.DataAnnotations;

namespace Internship.NetSiemens2025.domain.validator.concrete_validator;

/// <summary>
/// Validation class for borrowing item.
/// </summary>
public class BorrowingItemValidator : AbstractValidator<BorrowingItem>
{
    public override void Validate(BorrowingItem entity)
    {
        var validationActions = new List<Action>
        {
            () => ValidateQuantity(entity.Quantity)
        };
        
        RunValidation(validationActions);
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
            throw new ValidationException("Quantity cannot be negative or equal to zero!");
        }
    }
}