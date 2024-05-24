namespace Shared.Contracts;

public class CreateOrderRequest
{
    public Guid UserId { get; set; }
    [Range(1, int.MaxValue,ErrorMessage = "Необходимо выбрать тип оборудования!")]  public int EquipmentId { get; set; }
    [Range(1, int.MaxValue,ErrorMessage = "Минимальное время аренды 1 час!")] public int CountRentHours { get; set; }
    public DateTime Arrived { get; set; }
}