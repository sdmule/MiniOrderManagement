using MediatR;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Orders.Queries.GetAllOrders;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Orders.Queries.GetOrdersByCustomerId;

public class GetAllOrdersQueryHandler
    : IRequestHandler<
        GetAllOrdersQuery,
        IReadOnlyList<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOrdersQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<OrderDto>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orderRepository =
            _unitOfWork.Repository<Order>();

        var orders =
            await orderRepository.GetAllAsync(cancellationToken);   

        return orders
            .Select(x => new OrderDto(
                x.Id,
                x.CustomerId,
                x.OrderDate,
                x.TotalAmount))
            .ToList();
    }
}