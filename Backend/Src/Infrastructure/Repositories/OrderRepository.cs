namespace Infrastructure.Repositories;

public class OrderRepository(
    ApplicationDbContext dbContext)
    : IOrderRepository
{
    public async Task<PaginatedData<OrderEntity>> GetPaginatedOrdersWithIncludesAsync(
        int pageNumber, int pageSize, string? searchTerms)
    {
        var listQuery = dbContext.Orders
            .AsNoTracking();
        var countQuery = dbContext.Orders
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerms))
        {
            listQuery = listQuery
                .Where(x =>
                    x.User.LastName.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase) ||
                    x.User.FirstName.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase) ||
                    x.User.MiddleName.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Equipment.Name.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase));
            countQuery = countQuery
                .Where(x =>
                    x.User.LastName.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase) ||
                    x.User.FirstName.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase) ||
                    x.User.MiddleName.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Equipment.Name.Contains(searchTerms, StringComparison.CurrentCultureIgnoreCase));
        }
        var list = await listQuery
            .OrderByDescending(x => x.Created)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.User)
            .Include(x => x.User)
            .Include(x => x.Equipment)
            .Include(x => x.Status)
            .Include(x => x.Status)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
        var totalCount = await countQuery
            .CountAsync();
        return new PaginatedData<OrderEntity>(
            List: list, TotalCount: totalCount);
    }

    public async Task<OrderEntity?> GetByIdWithIncludesAsync(Guid id)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Equipment)
            .Include(x => x.Status)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<OrderEntity?> GetByIdAsync(Guid id)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(OrderEntity entity)
    {
        await dbContext.Orders.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderEntity entity)
    {
        dbContext.Orders.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(OrderEntity entity)
    {
        dbContext.Orders.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}