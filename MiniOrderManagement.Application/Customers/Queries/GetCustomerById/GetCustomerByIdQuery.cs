using MediatR;
using MiniOrderManagement.Application.DTOs;

namespace MiniOrderManagement.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(
    int CustomerId) : IRequest<CustomerDto?>;


//Other way using a class instead of a record
//public class GetCustomerByIdQuery : IRequest<CustomerDto?>
//{
//    public int CustomerId { get; }

//    public GetCustomerByIdQuery(int customerId)
//    {
//        CustomerId = customerId;
//    }
//}