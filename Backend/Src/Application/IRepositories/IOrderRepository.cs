namespace Application.IRepositories;

public interface IOrderRepository
{
    Task<PaginatedData<OrderEntity>> GetPaginatedOrdersWithIncludesAsync(
        int pageNumber, int pageSize, string? searchTerms);
    Task<OrderEntity?> GetByIdWithIncludesAsync(Guid id);
    Task<OrderEntity?> GetByIdAsync(Guid id);

    Task CreateAsync(OrderEntity entity);
    Task UpdateAsync(OrderEntity entity);
    Task DeleteAsync(OrderEntity entity);
}