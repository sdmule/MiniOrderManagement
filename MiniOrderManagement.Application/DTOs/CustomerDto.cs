namespace MiniOrderManagement.Application.DTOs;

public record CustomerDto(
    int Id,
    string Name,
    CustomerProfileDto? Profile,
    IEnumerable<OrderDto> Orders);