namespace Domain.Entities;

public class OrderEntity
    : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public int EquipmentId { get; set; }
    public int StatusId { get; set; }
    
    public int CountRentHours { get; set; }
    public decimal TotalCost { get; set; }
    public bool IsPayed { get; set; }
    public DateTime Arrived { get; set; }

    public UserEntity User { get; set; } = default!;
    public EquipmentEntity Equipment { get; set; } = default!;
    public OrderStatusEntity Status { get; set; } = default!;
}