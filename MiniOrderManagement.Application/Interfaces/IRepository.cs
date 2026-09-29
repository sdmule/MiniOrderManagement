using System.Linq.Expressions;

namespace MiniOrderManagement.Application.Interfaces;

public interface IRepository<T>
    where T : class
{
    Task<T?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);

    Task<List<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken);

    Task AddAsync(
        T entity,
        CancellationToken cancellationToken);

    // New: get all with optional include expressions
    Task<List<T>> GetAllAsync(
        CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);
}