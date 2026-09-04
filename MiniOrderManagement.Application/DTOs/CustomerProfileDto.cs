namespace MiniOrderManagement.Application.DTOs;

public record CustomerProfileDto(
    int Id,
    string Address,
    string PhoneNumber);