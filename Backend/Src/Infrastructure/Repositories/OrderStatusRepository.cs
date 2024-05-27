namespace Infrastructure.Repositories;

public class OrderStatusRepository(
    ApplicationDbContext dbContext)
    : IOrderStatusRepository
{
    public async Task<IEnumerable<OrderStatusEntity>> GetAllAsync()
    {
        return await dbContext.OrderStatuses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OrderStatusEntity?> GetByIdAsync(int id)
    {
        return await dbContext.OrderStatuses
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(OrderStatusEntity entity)
    {
        await dbContext.OrderStatuses.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderStatusEntity entity)
    {
        dbContext.OrderStatuses.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(OrderStatusEntity entity)
    {
        dbContext.OrderStatuses.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsExistByNameAsync(string name)
    {
        return await dbContext.OrderStatuses
            .AsNoTracking()
            .AnyAsync(x => x.Name == name);
    }

    public async Task<bool> IsExistForUpdateByName(int id, string name)
    {
        return await dbContext.OrderStatuses
            .AsNoTracking()
            .AnyAsync(x => x.Id != id && x.Name == name);
    }
}