using MediatR;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Orders.Queries.GetOrdersByCustomerId;

public class GetOrdersByCustomerIdQueryHandler
    : IRequestHandler<
        GetOrdersByCustomerIdQuery,
        IReadOnlyList<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersByCustomerIdQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<OrderDto>> Handle(
        GetOrdersByCustomerIdQuery request,
        CancellationToken cancellationToken)
    {
        var orderRepository =
            _unitOfWork.Repository<Order>();

        var orders =
            await orderRepository.FindAsync(
                x => x.CustomerId == request.CustomerId,
                cancellationToken);

        return orders
            .Select(x => new OrderDto(
                x.Id,
                x.CustomerId,
                x.OrderDate,
                x.TotalAmount))
            .ToList();
    }
}