using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniOrderManagement.Application.Customers.Commands.CreateCustomer;
using MiniOrderManagement.Application.Customers.Queries.GetCustomerById;

namespace MiniOrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;

    public CustomersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var customerId =
            await _sender.Send(
                command,
                cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = customerId },
            new { id = customerId });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetCustomerByIdQuery(id),
                cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}