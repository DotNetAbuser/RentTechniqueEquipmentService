namespace Shared.Contracts;

public record EquipmentResponse(
    int Id,
    string Name,
    string Description,
    string PicturePath,
    decimal CostOneHour);