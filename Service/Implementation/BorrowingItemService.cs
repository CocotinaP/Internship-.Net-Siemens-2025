using Internship.NetSiemens2025.domain;
using Internship.NetSiemens2025.persistence;

namespace Internship.NetSiemens2025.service.implementation;

/// <summary>
/// Service class for managing BorrowingItem entities.
/// Inherits generic CRUD operations from CRUDService and implements additional BorrowingItem-specific logic.
/// </summary>
/// <inheritdoc cref="CRUDService{TId,TEntity}"/>
public class BorrowingItemService : CRUDService<int, BorrowingItem>, IBorrowingItemService
{
    /// <summary>
    /// Initializes a new instance of the BorrowingItemService class.
    /// </summary>
    /// <param name="repository">
    /// Instance of IBorrowingItemRepository responsible for handling database interactions for BorrowingItem entities.
    /// </param>
    public BorrowingItemService(IBorrowingItemRepository repository) : base(repository)
    {
    }
}