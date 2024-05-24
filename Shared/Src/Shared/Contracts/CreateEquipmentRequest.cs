namespace Shared.Contracts;

public class CreateEquipmentRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    [Required] public decimal CostOneHour  { get; set; }
    public UploadRequest? EquipmentPictureUploadRequest { get; set; }
}