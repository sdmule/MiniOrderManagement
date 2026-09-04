namespace MiniOrderManagement.Domain.Entities;

public class Order
{
    public int Id { get; private set; }

    public int CustomerId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public decimal TotalAmount { get; private set; }

    public Customer Customer { get; private set; } = null!;

    private Order()
    {
    }

    public Order(
        int customerId,
        DateTime orderDate,
        decimal totalAmount)
    {
        if (customerId <= 0)
            throw new ArgumentException("Customer is required.");

        if (totalAmount <= 0)
            throw new ArgumentException(
                "Order total amount must be greater than zero.");

        CustomerId = customerId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
    }
}