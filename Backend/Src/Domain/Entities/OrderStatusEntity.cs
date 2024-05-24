namespace Domain.Entities;

public class OrderStatusEntity
    : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

    public List<OrderEntity> Orders { get; set; } = [];
}