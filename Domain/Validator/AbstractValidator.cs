using System.Text;
using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.domain.validator;

/// <summary>
/// Class for abstract validator.
/// </summary>
/// <typeparam name="TE">
/// Type of entity to validate.
/// </typeparam>
public abstract class AbstractValidator<TE> : IValidator<TE> where TE : class
{
    public abstract void Validate(TE entity);

    /// <summary>
    /// Executes all validation actions for an entity, collecting errors encountered.
    /// If any validation fails, all error messages are aggregated and thrown as a single exception.
    /// </summary>
    /// <param name="validationActions">
    /// A list of actions that contain individual validations.
    /// Each action is executed independently.
    /// </param>
    /// <exception cref="ValidationException">
    /// Thrown if one or more validations fail.
    /// The exception message will include all accumulated validation errors.
    /// </exception>

    protected void RunValidation(List<Action> validationActions)
    {
        var errorMessage = new StringBuilder();
        
        foreach (var action in validationActions)
        {
            try
            {
                action.Invoke();
            }
            catch (ValidationException validationException)
            {
                errorMessage.AppendLine(validationException.Message);
            }
        }

        if (errorMessage.Length > 0)
        {
            throw new ValidationException(errorMessage.ToString());
        }
    }
}