using MediatR;
using MiniOrderManagement.Application.DTOs;

namespace MiniOrderManagement.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(
    int OrderId) : IRequest<OrderDto?>;