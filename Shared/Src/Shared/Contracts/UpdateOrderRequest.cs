namespace Shared.Contracts;

public class UpdateOrderRequest
{
    public int EquipmentId { get; set; }
    public int CountRentHours { get; set; }
    public DateTime Arrived { get; set; }
}