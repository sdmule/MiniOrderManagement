namespace MiniOrderManagement.Application.Interfaces;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>()
        where T : class;

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}