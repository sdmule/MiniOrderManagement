using MediatR;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;


namespace MiniOrderManagement.Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerRepository = _unitOfWork.Repository<Customer>();

        // new repository method that supports includes
        var customers = await customerRepository.GetAllAsync(
            cancellationToken,
            x => x.Profile!,
            x => x.Orders);

        // map to DTOs
        return customers.Select(customer =>
            new CustomerDto(
                customer.Id,
                customer.Name,
                customer.Profile == null
                    ? null
                    : new CustomerProfileDto(
                        customer.Profile.Id,
                        customer.Profile.Address,
                        customer.Profile.PhoneNumber),
                customer.Orders.Select(order =>
                    new OrderDto(
                        order.Id,
                        order.CustomerId,
                        order.OrderDate,
                        order.TotalAmount))))
            .ToList();
    }
}