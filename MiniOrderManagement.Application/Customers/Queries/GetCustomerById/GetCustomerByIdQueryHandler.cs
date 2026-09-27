using MediatR;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByIdQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDto?> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customerRepository =
            _unitOfWork.Repository<Customer>();

        var customer =
            await customerRepository.GetByIdAsync(
                request.CustomerId,
                cancellationToken,
                x => x.Profile!,
                x => x.Orders);

        if (customer is null)
            return null;

        return new CustomerDto(
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
                    order.TotalAmount)));
    }
}