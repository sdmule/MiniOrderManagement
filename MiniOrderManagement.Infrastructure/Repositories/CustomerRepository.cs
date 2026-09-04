using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;
using MiniOrderManagement.Infrastructure.Persistence;

namespace MiniOrderManagement.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Customer customer,
        CancellationToken cancellationToken)
    {
        await _context.Customers.AddAsync(
            customer,
            cancellationToken);
    }

    public async Task<Customer?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Customer?> GetWithDetailsAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Include(x => x.Profile)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}