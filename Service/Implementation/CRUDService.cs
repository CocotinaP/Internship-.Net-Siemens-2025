using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.persistence;

namespace Internship.NetSiemens2025.service.implementation;

/// <summary>
/// Generic service class that implements CRUD operations for entities.
/// </summary>
/// <typeparam name="TId">
/// Type of the identifier used for the entity.
/// </typeparam>
/// <typeparam name="TEntity">
/// Type of the entity, which must inherit from Entity&lt;TId&gt;.
/// </typeparam>
public class CRUDService<TId, TEntity> : ICRUDService<TId, TEntity> where TEntity : Entity<TId>
{
    protected readonly IRepository<TId, TEntity> _repository;

    /// <summary>
    /// Initializes a new instance of the CRUDService class.
    /// </summary>
    /// <param name="repository">
    /// Instance of IRepository&lt;TId, TEntity&gt; responsible for interacting with the database.
    /// </param>
    public CRUDService(IRepository<TId, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual void Add(TEntity entity)
    {
        _repository.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _repository.Update(entity);
    }

    public virtual TEntity? Delete(TId id)
    {
       return _repository.Delete(id);
    }

    public virtual List<TEntity> GetAll()
    {
        return _repository.GetAll();
    }

    public virtual TEntity? GetById(TId id)
    {
        return _repository.GetById(id);
    }
}