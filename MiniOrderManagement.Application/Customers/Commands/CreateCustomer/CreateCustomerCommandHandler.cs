using MediatR;
using Microsoft.Extensions.Logging;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Domain.Entities;

namespace MiniOrderManagement.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler
    : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;

    public CreateCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreateCustomerCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating customer with name {CustomerName}",
            request.Name);

        var customer = new Customer
        {
            Name = request.Name
        };

        var profile = new CustomerProfile
        {
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        customer.Profile = profile;

        var customerRepository =
            _unitOfWork.Repository<Customer>();

        await customerRepository.AddAsync(
            customer,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        _logger.LogInformation(
            "Customer {CustomerId} created successfully",
            customer.Id);

        return customer.Id;
    }
}