using System.Text;
using System.Text.RegularExpressions;
using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.domain.validator.util;

/// <summary>
/// Useful class for validation.
/// </summary>
public partial class ValidatorUtils
{
    [GeneratedRegex(@"^[A-Za-z,.\s]+$")]
    private static partial Regex MyNameRegex();
    
    [GeneratedRegex(@"^\+?[0-9]{7,15}$")]
    private static partial Regex MyPhoneRegex();
    
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex MyEmailRegex();
    
    /// <summary>
    /// Validate name.
    /// </summary>
    /// <param name="name">
    /// Name to validate
    /// </param>
    /// <exception cref="ValidationException">
    /// If the name is not valid.
    /// </exception>
    public  static void ValidateName(string name)
    {
        var errorMessage = new StringBuilder();

        if (string.IsNullOrEmpty(name))
        {
            errorMessage.AppendLine("Name is required!");
        }

        if (!MyNameRegex().IsMatch(name))
        {
            errorMessage.AppendLine("Name is invalid!");
        }

        if (errorMessage.Length > 0)
        {
            throw new ValidationException(errorMessage.ToString());
        }
    }

    /// <summary>
    /// Validate phone number.
    /// </summary>
    /// <param name="phoneNumber">
    /// Phone number to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the phone number is not valid.
    /// </exception>
    public static void ValidatePhoneNumber(string phoneNumber)
    {
        var errorMessage = new StringBuilder();

        if (phoneNumber.Length < 10)
        {
            errorMessage.AppendLine("Phone number length is invalid!");
        }

        if (!MyPhoneRegex().IsMatch(phoneNumber))
        {
            errorMessage.AppendLine("Phone number is invalid!");
        }
        
        if (errorMessage.Length > 0)
        {
            throw new ValidationException(errorMessage.ToString());
        }
    }

    /// <summary>
    /// Validate email.
    /// </summary>
    /// <param name="email">
    /// Email to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the email is not valid.
    /// </exception>
    public static void ValidateEmail(string email)
    {
        if (!MyEmailRegex().IsMatch(email))
        {
            throw new ValidationException("Invalid email address!");
        }
    }
}