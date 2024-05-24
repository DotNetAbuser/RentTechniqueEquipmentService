namespace Domain.Entities;

public class SessionEntity
    : BaseEntity<int>
{
    public Guid UserId { get; set; }
    
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }

    public UserEntity User { get; set; } = default!;
}