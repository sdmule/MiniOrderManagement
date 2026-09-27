using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using MiniOrderManagement.Application.Customers.Commands.CreateCustomer;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Application.Orders.Commands.CreateOrder;
using MiniOrderManagement.Domain.Entities;
using Xunit;

namespace MiniOrderManagement.Tests.Commands;

public class CreateOrderCommandHandlerTests
{
    //Tests the command handler responsible for creating an order
    [Fact]
    public async Task CreateOrder_Should_Create_Order_When_CustomerExists()
    {
        // Arrange

        var customer = new Customer
        {
            Name = "Saurabh"
        };

        var customerRepository =
    new Mock<IRepository<Customer>>();

        var orders =
            new Mock<IRepository<Order>>();

        var unitOfWork =
            new Mock<IUnitOfWork>();

        var logger =
            new Mock<ILogger<CreateOrderCommandHandler>>();

        customerRepository
            .Setup(x => x.GetByIdAsync(
                1,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        unitOfWork
            .Setup(x => x.Repository<Customer>())
            .Returns(customerRepository.Object);

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(orders.Object);

        var handler = new CreateOrderCommandHandler(
            unitOfWork.Object,
            logger.Object);

        var orderDate = new DateTime(2026, 9, 4, 10, 30, 0);

        var command = new CreateOrderCommand(
            1,
            orderDate,
            1000);

        Order? capturedOrder = null;

        orders
            .Setup(x => x.AddAsync(
                It.IsAny<Order>(),
                It.IsAny<CancellationToken>()))
            .Callback<Order, CancellationToken>((order, _) =>
            {
                capturedOrder = order;
            })
            .Returns(Task.CompletedTask);

        // Act

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert

        Assert.NotNull(capturedOrder);

        Assert.Equal(1, capturedOrder.CustomerId);
        Assert.Equal(orderDate, capturedOrder.OrderDate);
        Assert.Equal(1000, capturedOrder.TotalAmount);

        orders.Verify(
            x => x.AddAsync(
                It.IsAny<Order>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    //Negative test as Customer must exist before CreateOrder
    [Fact]
    public async Task CreateOrder_Should_Throw_When_CustomerDoesNotExist()
    {
        // Arrange

        var customers = new Mock<IRepository<Customer>>();
        var orders = new Mock<IRepository<Order>>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateOrderCommandHandler>>();

        customers
            .Setup(x => x.GetByIdAsync(
                9999,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        unitOfWork
            .Setup(x => x.Repository<Customer>())
            .Returns(customers.Object);

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(orders.Object);

        var handler = new CreateOrderCommandHandler(
            unitOfWork.Object,
            logger.Object);

        var command = new CreateOrderCommand(
            9999,
            new DateTime(2026, 9, 4, 10, 30, 0),
            500);

        // Act & Assert

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => handler.Handle(
                command,
                CancellationToken.None));

        orders.Verify(
            x => x.AddAsync(
                It.IsAny<Order>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}