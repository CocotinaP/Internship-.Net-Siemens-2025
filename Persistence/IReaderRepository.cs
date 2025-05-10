using Internship.NetSiemens2025.domain;

namespace Internship.NetSiemens2025.persistence;

/// <summary>
/// Repository interface for managing Reader entities.
/// Provides data access operations specific to Reader objects.
/// </summary>
/// <inheritdoc cref="IRepository{TId,TEntity}"/>
public interface IReaderRepository : IRepository<int, Reader>
{
    
}