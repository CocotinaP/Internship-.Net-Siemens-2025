namespace Internship.NetSiemens2025.exception;

/// <summary>
/// Initializes a new instance of the ValidationException class with a specified error message.
/// </summary>
/// <param name="message">
/// A message that describes the error.
/// </param>
public class ValidationException(string message) : ApplicationException(message);