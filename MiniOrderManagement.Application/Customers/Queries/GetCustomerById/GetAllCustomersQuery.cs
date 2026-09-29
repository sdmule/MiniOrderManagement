using MediatR;
using MiniOrderManagement.Application.DTOs;

namespace MiniOrderManagement.Application.Customers.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery() : IRequest<List<CustomerDto>>;