namespace MiniOrderManagement.Domain.Entities;

public class Customer
{
    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public CustomerProfile? Profile { get; private set; }

    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    private Customer()
    {
    }

    public Customer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Customer name is required.", nameof(name));

        Name = name;
    }

    public void AddProfile(CustomerProfile profile)
    {
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
    }
}