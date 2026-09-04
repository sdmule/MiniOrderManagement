using Castle.Core.Resource;
using MiniOrderManagement.Application.Customers.Queries.GetCustomerById;
using MiniOrderManagement.Application.DTOs;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;
using Moq;

namespace MiniOrderManagement.Tests.Queries;

public class GetCustomerByIdQueryHandlerTests
{
    //This test verifies that:
    //When we request a customer by ID, the GetCustomerByIdQueryHandler correctly retrieves the customer and maps the Customer +CustomerProfile + Orders into CustomerDto.
    [Fact]
    public async Task GetCustomerById_Should_Return_Customer_With_Profile_And_Orders()
    {
        // Arrange

        var customer = new Customer("Saurabh");

        var profile = new CustomerProfile(
            "Bangalore",
            "9876543210");

        customer.AddProfile(profile);

        var order1 = new Order(
            1,
            new DateTime(2026, 9, 4),
            500);

        var order2 = new Order(
            1,
            new DateTime(2026, 9, 3),
            1000);

        customer.Orders.Add(order1);
        customer.Orders.Add(order2);

        var customers = new Mock<ICustomerRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        customers
            .Setup(x => x.GetWithDetailsAsync(
                1,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        unitOfWork
            .Setup(x => x.Customers)
            .Returns(customers.Object);

        var handler = new GetCustomerByIdQueryHandler(
            unitOfWork.Object);

        var query = new GetCustomerByIdQuery(1);

        // Act

        var result = await handler.Handle(
            query,
            CancellationToken.None);

        // Assert

        Assert.NotNull(result);

        Assert.Equal("Saurabh", result.Name);

        Assert.NotNull(result.Profile);
        Assert.Equal("Bangalore", result.Profile.Address);
        Assert.Equal("9876543210", result.Profile.PhoneNumber);

        Assert.Equal(2, result.Orders.Count());

        Assert.Contains(
            result.Orders,
            order => order.TotalAmount == 500);

        Assert.Contains(
            result.Orders,
            order => order.TotalAmount == 1000);

        customers.Verify(
            x => x.GetWithDetailsAsync(
                1,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    //Query "not found" test
    [Fact]
    public async Task GetCustomerById_Should_Return_Null_When_CustomerDoesNotExist()
    {
        // Arrange

        var customers = new Mock<ICustomerRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        customers
            .Setup(x => x.GetWithDetailsAsync(
                9999,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        unitOfWork
            .Setup(x => x.Customers)
            .Returns(customers.Object);

        var handler = new GetCustomerByIdQueryHandler(
            unitOfWork.Object);

        var query = new GetCustomerByIdQuery(9999);

        // Act

        var result = await handler.Handle(
            query,
            CancellationToken.None);

        // Assert

        Assert.Null(result);

        customers.Verify(
            x => x.GetWithDetailsAsync(
                9999,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}