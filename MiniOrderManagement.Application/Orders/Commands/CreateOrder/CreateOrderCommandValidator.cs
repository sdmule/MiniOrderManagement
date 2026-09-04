using FluentValidation;

namespace MiniOrderManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("CustomerId must be greater than zero.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Order total must be greater than zero.");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order date is required.");
    }
}