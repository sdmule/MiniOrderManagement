namespace MiniOrderManagement.Domain.Entities;

public class CustomerProfile
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public Customer Customer { get; set; } = null!;
}