using MediatR;
using MiniOrderManagement.Application.DTOs;

namespace MiniOrderManagement.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IRequest<IReadOnlyList<OrderDto>>;