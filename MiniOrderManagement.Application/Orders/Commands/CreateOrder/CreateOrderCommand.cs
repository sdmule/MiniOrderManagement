using MediatR;

namespace MiniOrderManagement.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    int CustomerId,
    DateTime OrderDate,
    decimal TotalAmount) : IRequest<int>;