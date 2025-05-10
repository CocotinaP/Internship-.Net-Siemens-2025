using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.domain.validator;

/// <summary>
/// Interface for validation.
/// </summary>
/// <typeparam name="TEntity">
/// The type of the entity to validate.
/// </typeparam>
public interface IValidator<TEntity> where TEntity : class
{ 
    /// <summary>
    /// Validate an entity.
    /// </summary>
    /// <param name="entity">
    /// The entity to validate.
    /// </param>
    /// <exception cref="ValidationException">
    /// If the entity is not valid.
    /// </exception>
    public void Validate(TEntity entity);
}