using System.Linq.Expressions;
using Moq;
using MiniOrderManagement.Application.Customers.Queries.GetCustomerById;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Tests.Queries;

public class GetCustomerByIdQueryHandlerTests
{
    // Tests that the customer, profile and orders
    // are correctly retrieved and mapped to CustomerDto.
    [Fact]
    public async Task GetCustomerById_Should_Return_Customer_With_Profile_And_Orders()
    {
        // Arrange

        var customer = new Customer
        {
            Name = "Saurabh"
        };

        var profile = new CustomerProfile
        {
            Address = "Bangalore",
            PhoneNumber = "9876543210",
            CustomerId = 1
        };

        customer.Profile = profile;

        var order1 = new Order
        {
            CustomerId = 1,
            OrderDate = new DateTime(2026, 9, 4),
            TotalAmount = 500
        };

        var order2 = new Order
        {
            CustomerId = 1,
            OrderDate = new DateTime(2026, 9, 3),
            TotalAmount = 1000
        };

        customer.Orders.Add(order1);
        customer.Orders.Add(order2);

        var customerRepository =
            new Mock<IRepository<Customer>>();

        var unitOfWork =
            new Mock<IUnitOfWork>();

        customerRepository
            .Setup(x => x.GetByIdAsync(
                1,
                It.IsAny<CancellationToken>(),
                It.IsAny<Expression<Func<Customer, object>>[]>()))
            .ReturnsAsync(customer);

        unitOfWork
            .Setup(x => x.Repository<Customer>())
            .Returns(customerRepository.Object);

        var handler =
            new GetCustomerByIdQueryHandler(
                unitOfWork.Object);

        var query =
            new GetCustomerByIdQuery(1);

        // Act

        var result =
            await handler.Handle(
                query,
                CancellationToken.None);

        // Assert

        Assert.NotNull(result);

        Assert.Equal(
            "Saurabh",
            result.Name);

        Assert.NotNull(result.Profile);

        Assert.Equal(
            "Bangalore",
            result.Profile.Address);

        Assert.Equal(
            "9876543210",
            result.Profile.PhoneNumber);

        Assert.Equal(
            2,
            result.Orders.Count());

        Assert.Contains(
            result.Orders,
            order => order.TotalAmount == 500);

        Assert.Contains(
            result.Orders,
            order => order.TotalAmount == 1000);

        customerRepository.Verify(
            x => x.GetByIdAsync(
                1,
                It.IsAny<CancellationToken>(),
                It.IsAny<Expression<Func<Customer, object>>[]>()),
            Times.Once);
    }


    // Tests that null is returned when
    // the requested customer does not exist.
    [Fact]
    public async Task GetCustomerById_Should_Return_Null_When_CustomerDoesNotExist()
    {
        // Arrange

        var customerRepository =
            new Mock<IRepository<Customer>>();

        var unitOfWork =
            new Mock<IUnitOfWork>();

        customerRepository
            .Setup(x => x.GetByIdAsync(
                9999,
                It.IsAny<CancellationToken>(),
                It.IsAny<Expression<Func<Customer, object>>[]>()))
            .ReturnsAsync((Customer?)null);

        unitOfWork
            .Setup(x => x.Repository<Customer>())
            .Returns(customerRepository.Object);

        var handler =
            new GetCustomerByIdQueryHandler(
                unitOfWork.Object);

        var query =
            new GetCustomerByIdQuery(9999);

        // Act

        var result =
            await handler.Handle(
                query,
                CancellationToken.None);

        // Assert

        Assert.Null(result);

        customerRepository.Verify(
            x => x.GetByIdAsync(
                9999,
                It.IsAny<CancellationToken>(),
                It.IsAny<Expression<Func<Customer, object>>[]>()),
            Times.Once);
    }
}