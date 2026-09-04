using MediatR;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;

namespace MiniOrderManagement.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDto?> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customer =
            await _unitOfWork.Customers.GetWithDetailsAsync(
                request.CustomerId,
                cancellationToken);

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