using MediatR;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Orders.Queries.GetOrderById;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Orders.Queries.GetOrdersByCustomerId;

public class GetOrderByIdQueryHandler
    : IRequestHandler<
        GetOrderByIdQuery,
        OrderDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderByIdQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDto?> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var orderRepository =
            _unitOfWork.Repository<Order>();

        var order =
            await orderRepository.GetByIdAsync(
                request.OrderId,
                cancellationToken);

        if (order is null)
            return null;

        return new OrderDto(
            order.Id,
            order.CustomerId,
            order.OrderDate,
            order.TotalAmount);
    }
}