using MediatR;
using Microsoft.Extensions.Logging;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating order for customer {CustomerId}",
            request.CustomerId);

        var customer = await _unitOfWork.Customers
            .GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            _logger.LogWarning(
                "Customer {CustomerId} was not found while creating an order",
                request.CustomerId);

            throw new KeyNotFoundException(
                $"Customer with ID {request.CustomerId} was not found.");
        }

        var order = new Order(
            request.CustomerId,
            request.OrderDate,
            request.TotalAmount);

        await _unitOfWork.Orders.AddAsync(
            order,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Order {OrderId} created successfully for customer {CustomerId}",
            order.Id,
            request.CustomerId);

        return order.Id;
    }
}