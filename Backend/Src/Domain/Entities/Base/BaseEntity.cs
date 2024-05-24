namespace Domain.Entities.Base;

public class BaseEntity<TId>
{
    public TId Id { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }
}