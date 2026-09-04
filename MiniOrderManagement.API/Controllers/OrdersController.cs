using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Orders.Commands.CreateOrder;
using MiniOrderManagement.Application.Orders.Queries.GetOrdersByCustomerId;

namespace MiniOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var orderId =
            await _sender.Send(
                command,
                cancellationToken);

        return Created(
            $"/api/orders/{orderId}",
            new { id = orderId });
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<IActionResult> GetByCustomer(
        int customerId,
        CancellationToken cancellationToken)
    {
        var orders =
            await _sender.Send(
                new GetOrdersByCustomerIdQuery(customerId),
                cancellationToken);

        return Ok(orders);
    }
}