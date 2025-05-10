using Internship.NetSiemens2025.domain;

namespace Internship.NetSiemens2025.service;

/// <summary>
/// Interface for managing readers.
/// Inherits from ICRUDService and provides standard CRUD operations.
/// </summary>
/// <inheritdoc cref="ICRUDService{TId,TEntity}"/>
public interface IReaderService : ICRUDService<int, Reader>
{
    
}