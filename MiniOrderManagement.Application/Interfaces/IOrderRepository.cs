using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(
        Order order,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken);
}