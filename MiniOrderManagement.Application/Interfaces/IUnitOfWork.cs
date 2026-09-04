namespace MiniOrderManagement.Application.Interfaces;

public interface IUnitOfWork
{
    ICustomerRepository Customers { get; }

    IOrderRepository Orders { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken);
}