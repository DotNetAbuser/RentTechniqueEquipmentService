namespace Shared.Contracts;

public class UpdateEquipmentRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public decimal CostOneHour  { get; set; }
}