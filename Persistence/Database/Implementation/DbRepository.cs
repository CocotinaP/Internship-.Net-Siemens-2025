using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.domain.validator;

namespace Internship.NetSiemens2025.persistence.database.implementation;

/// <summary>
/// Generic repository class for basic operations.
/// </summary>
/// <typeparam name="TId">The type of unique identifier of an entity.</typeparam>
/// <typeparam name="TEntity">The type of the entity to be stored.</typeparam>
public class DbRepository<TId, TEntity> : IRepository<TId,TEntity> where TEntity : Entity<TId>
{
    protected IValidator<TEntity> Validator;
    protected LibraryDbContext Context;

    /// <summary>
    /// Constructor for the DbRepository class.
    /// Initializes a repository with a validator and a database context.
    /// </summary>
    /// <param name="validator">
    /// Instance of IValidator&lt;TEntity&gt;, used for validating entities before they are persisted.
    /// </param>
    /// <param name="context">
    /// Instance of LibraryDbContext, used for interacting with the database.
    /// </param>
    public DbRepository(IValidator<TEntity> validator, LibraryDbContext context)
    {
        this.Validator = validator;
        this.Context = context;
    }
    
    public virtual void Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        Validator.Validate(entity);
        Context.Add(entity);
        Context.SaveChanges();
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        var existingEntity = Context.Set<TEntity>().Find(entity.Id);

        if (existingEntity != null)
        {
            Context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            Validator.Validate(entity);
            Context.Set<TEntity>().Update(entity);
        }

        Context.SaveChanges();
    }

    public virtual TEntity? Delete(TId id)
    {
        var entity = Context.Find<TEntity>(id);

        if (entity != null)
        {
            Context.Remove(entity);
        }
        
        Context.SaveChanges();
        
        return entity;
    }

    public virtual List<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public virtual TEntity? GetById(TId id)
    {
        return Context.Set<TEntity>().Find(id);
    }
}