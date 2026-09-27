using MediatR;
using Microsoft.Extensions.Logging;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, int>
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

        var customerRepository = _unitOfWork.Repository<Customer>();

        var customer = await customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken);

        if (customer is null)
        {
            _logger.LogWarning(
                "Customer {CustomerId} not found",
                request.CustomerId);

            throw new KeyNotFoundException(
                $"Customer with id {request.CustomerId} not found");
        }

        var order = new Order
        {
            CustomerId = request.CustomerId,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount
        };

        var orderRepository = _unitOfWork.Repository<Order>();

        await orderRepository.AddAsync(order, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Order created for customer {CustomerId} with id {OrderId}",
            request.CustomerId,
            order.Id);

        return order.Id;
    }
}
