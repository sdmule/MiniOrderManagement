using MediatR;
using MiniOrderManagement.Application.DTOs;

namespace MiniOrderManagement.Application.Orders.Queries.GetOrdersByCustomerId;

public record GetOrdersByCustomerIdQuery(
    int CustomerId) : IRequest<IReadOnlyList<OrderDto>>;