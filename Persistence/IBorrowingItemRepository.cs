using Internship.NetSiemens2025.domain;

namespace Internship.NetSiemens2025.persistence;

/// <summary>
/// Repository interface for managing Borrowing Item entities.
/// Provides data access operations specific to Borrowing Item objects.
/// </summary>
/// <inheritdoc cref="IRepository{TId,TEntity}"/>
public interface IBorrowingItemRepository : IRepository<int, BorrowingItem>
{
}