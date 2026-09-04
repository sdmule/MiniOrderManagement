namespace MiniOrderManagement.Application.DTOs;

public record OrderDto(
    int Id,
    int CustomerId,
    DateTime OrderDate,
    decimal TotalAmount);