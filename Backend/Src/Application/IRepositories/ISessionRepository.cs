namespace Application.IRepositories;

public interface ISessionRepository
{
    Task<SessionEntity?> GetByRefreshTokenAsync(string token);

    Task CreateAsync(SessionEntity entity);
    Task DeleteAsync(SessionEntity entity);
}