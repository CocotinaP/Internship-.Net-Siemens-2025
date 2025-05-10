using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.exception;

namespace Internship.NetSiemens2025.service;

/// <summary>
/// Generic interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="TId">
/// Type of the identifier used for the entity.
/// </typeparam>
/// <typeparam name="TEntity">
/// Type of the entity, which must inherit from Entity&lt;TId&gt;.
/// </typeparam>
public interface ICRUDService<TId, TEntity> where TEntity : Entity<TId>
{
    /// <summary>
    /// Add an entity.
    /// </summary>
    /// <param name="entity">
    /// Entity to add.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If the given entity is null.
    /// </exception>
    /// <exception cref="ValidationException">
    /// If the given entity is not valid.
    /// </exception>
    public void Add(TEntity entity);
    
    /// <summary>
    /// Update an entity.
    /// </summary>
    /// <param name="entity">
    /// Entity to update.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If the given entity is null.
    /// </exception>
    /// <exception cref="ValidationException">
    /// If the given entity is not valid.
    /// </exception>
    public void Update(TEntity entity);
    
    /// <summary>
    /// Delete an entity.
    /// </summary>
    /// <param name="id">
    /// The id of the entity to delete.
    /// </param>
    /// <returns>
    /// The deleted entity if found.
    /// </returns>
    public TEntity? Delete(TId id);

    /// <summary>
    /// Get all entities.
    /// </summary>
    /// <returns>
    /// All entities.
    /// </returns>
    public List<TEntity> GetAll();
    
    /// <summary>
    /// Find an entity by id.
    /// </summary>
    /// <param name="id">
    /// The id of the entity to find.
    /// </param>
    /// <returns>
    /// The entity with the given id or null.
    /// </returns>
    public TEntity? GetById(TId id);
}