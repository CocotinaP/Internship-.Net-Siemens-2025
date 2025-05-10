using System.Text;
using Internship.NetSiemens2025.domain.validator.util;
using Internship.NetSiemens2025.exception;
using Internship.NetSiemens2025.util;

namespace Internship.NetSiemens2025.domain.validator.concrete_validator;

/// <summary>
/// Validation class for reader.
/// </summary>
public class ReaderValidator : AbstractValidator<Reader>
{
    public override void Validate(Reader entity)
    {
        var validationActions = new List<Action>
        {
            () => ValidateCnp(entity.Cnp),
            () => ValidateName(entity.FirstName, entity.LastName),
            () => ValidatePhoneNumber(entity.PhoneNumber),
            () => ValidateEmail(entity.Email),
        };

        RunValidation(validationActions);
    }

    /// <summary>
    /// Validating the reader's cnp.
    /// </summary>
    /// <param name="cnp">
    /// Cnp to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the cnp is not valid.
    /// </exception>
    private void ValidateCnp(string cnp)
    {
        var errorMessage = new StringBuilder();
        
        if (cnp.Length != 13)
        {
            errorMessage.AppendLine("CNP number must have 13 characters!");
        }

        CnpUtils.ExtractDateFromCnp(cnp);
    }

    /// <summary>
    /// Validating the reader's name.
    /// </summary>
    /// <param name="names">
    /// Name to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the name is not valid.
    /// </exception>
    private void ValidateName(params string[] names)
    {
        foreach (var name in names)
        {
            ValidatorUtils.ValidateName(name);
        }
    }

    /// <summary>
    /// Validating the reader's phone number.
    /// </summary>
    /// <param name="phoneNumber">
    /// Phone number to validate.</param>
    /// <exception cref="ValidationException">
    /// If the phone number is not valid.</exception>
    private void ValidatePhoneNumber(string phoneNumber)
    {
        ValidatorUtils.ValidatePhoneNumber(phoneNumber);
    }

    /// <summary>
    /// Validating the reader's email.
    /// </summary>
    /// <param name="email">
    /// Email to validate.</param>
    /// <exception cref="ValidationException">
    /// If the email is not valid.</exception>
    private void ValidateEmail(string email)
    {
        ValidatorUtils.ValidateEmail(email);
    }
}