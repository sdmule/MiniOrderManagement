using MediatR;

namespace MiniOrderManagement.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string Name,
    string Address,
    string PhoneNumber) : IRequest<int>;