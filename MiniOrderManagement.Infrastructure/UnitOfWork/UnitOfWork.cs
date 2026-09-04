using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Infrastructure.Persistence;
using MiniOrderManagement.Infrastructure.Repositories;

namespace MiniOrderManagement.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICustomerRepository Customers { get; }

    public IOrderRepository Orders { get; }

    public UnitOfWork(
        AppDbContext context,
        ICustomerRepository customers,
        IOrderRepository orders)
    {
        _context = context;
        Customers = customers;
        Orders = orders;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(
            cancellationToken);
    }
}