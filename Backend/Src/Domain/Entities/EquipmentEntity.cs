namespace Domain.Entities;

public class EquipmentEntity
    : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public decimal CostOneHour { get; set; }

    public List<OrderEntity> Orders { get; set; } = [];
}