using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.util;

/// <summary>
/// Utility class for handling operations related to Romanian Personal Numeric Codes (CNP).
/// </summary>
public class CnpUtils
{
    /// <summary>
    /// Extracts the birthdate from a given Romanian Personal Numeric Code (CNP).
    /// </summary>
    /// <param name="cnp">
    /// The Romanian Personal Numeric Code from which the birthdate is extracted.
    /// Must be a valid 13-digit numeric string.
    /// </param>
    /// <returns>
    /// A DateTime object representing the birthdate extracted from the CNP.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown if the CNP is invalid or does not contain a valid date.
    /// </exception>
    public static DateTime ExtractDateFromCnp(string cnp)
    {
        var century = cnp[0] switch
        {
            '1' or '2' => 1900,
            '5' or '6' => 2000,
            _ => 0
        };

        if (century == 0)
        {
            throw new ValidationException("Invalid CNP number!");
        }
        
        var year = century + int.Parse(cnp.Substring(1, 2));
        var month = int.Parse(cnp.Substring(3, 2));
        var day = int.Parse(cnp.Substring(5, 2));

        try
        {
            return new DateTime(year, month, day);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            throw new ValidationException("Invalid CNP number!");
        }
    }
}