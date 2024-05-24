namespace Application.IServices;

public interface IOrderStatusService
{
    Task<Result<IEnumerable<OrderStatusResponse>>> GetAllAsync();
    
    Task<Result<OrderStatusResponse>> GetByIdAsync(int id);
    
    Task<Result> CreateAsync(CreateOrderStatusRequest request);
    Task<Result> UpdateAsync(int id, UpdateOrderStatusRequest request);
    Task<Result> DeleteAsync(int id);
}