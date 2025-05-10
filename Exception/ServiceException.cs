namespace Internship.NetSiemens2025.exception;

/// <summary>
/// Initializes a new instance of the ServiceException class with a specified error message.
/// </summary>
/// <param name="message">
/// A message that describes the error.
/// </param>
public class ServiceException(string message) : ApplicationException(message);