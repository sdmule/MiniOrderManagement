namespace MiniOrderManagement.Domain.Entities;

public class CustomerProfile
{
    public int Id { get; private set; }

    public int CustomerId { get; private set; }

    public string Address { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public Customer Customer { get; private set; } = null!;

    private CustomerProfile()
    {
    }

    public CustomerProfile(
        string address,
        string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.");

        Address = address;
        PhoneNumber = phoneNumber;
    }
}