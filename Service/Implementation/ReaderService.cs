using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.persistence;

namespace Internship.NetSiemens2025.service.implementation;

/// <summary>
/// Service class for managing Reader entities.
/// Inherits generic CRUD operations from CRUDService and implements additional reader-specific logic.
/// </summary>
/// <inheritdoc cref="CRUDService{TId,TEntity}"/>
public class ReaderService : CRUDService<int, Reader>, IReaderService
{
    /// <summary>
    /// Initializes a new instance of the ReaderService class.
    /// </summary>
    /// <param name="repository">
    /// Instance of IReaderRepository responsible for handling database interactions for Reader entities.
    /// </param>
    public ReaderService(IReaderRepository repository) : base(repository)
    {
    }
}