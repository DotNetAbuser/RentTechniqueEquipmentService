namespace Application.IRepositories;

public interface IOrderStatusRepository
{
    Task<IEnumerable<OrderStatusEntity>> GetAllAsync();
    Task<OrderStatusEntity?> GetByIdAsync(int id);
    
    Task CreateAsync(OrderStatusEntity entity);
    Task UpdateAsync(OrderStatusEntity entity);
    Task DeleteAsync(OrderStatusEntity entity);
    
    Task<bool> IsExistByNameAsync(string name);
    Task<bool> IsExistForUpdateByName(int id, string name);
}