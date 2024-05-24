namespace Infrastructure.Repositories;

public class SessionRepository(
    ApplicationDbContext dbContext)
    : ISessionRepository
{
    public async Task<SessionEntity?> GetByRefreshTokenAsync(string token)
    {
        return await dbContext.Sessions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Token == token);
    }

    public async Task CreateAsync(SessionEntity entity)
    {
        await dbContext.Sessions.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(SessionEntity entity)
    {
        dbContext.Sessions.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}