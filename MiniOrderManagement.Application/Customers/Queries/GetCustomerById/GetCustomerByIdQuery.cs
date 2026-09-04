using MediatR;
using MiniOrderManagement.Application.DTOs;

namespace MiniOrderManagement.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(
    int CustomerId) : IRequest<CustomerDto?>;