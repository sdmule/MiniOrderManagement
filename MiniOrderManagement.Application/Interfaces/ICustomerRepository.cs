using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<Customer?> GetWithDetailsAsync(
        int id,
        CancellationToken cancellationToken);

    Task AddAsync(
        Customer customer,
        CancellationToken cancellationToken);
}