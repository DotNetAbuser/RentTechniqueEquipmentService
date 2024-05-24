namespace Shared.Contracts;

public record OrderResponse(
    string LastName,
    string FirstName,
    string MiddleName,
    string email,
    string phoneNumber,
    string EquipmentName,
    string StatusName, 
    decimal TotalCost,
    bool IsPayed,
    DateTime Arrived,
    DateTime Created);