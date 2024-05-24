namespace Application.IServices;

public interface IOrderService
{
    Task<Result<PaginatedData<OrderResponse>>> GetPaginatedOrdersAsync(
        int pageNumber, int pageSize, string? searchTerms);

    Task<Result<OrderResponse>> GetByIdAsync(Guid id);
    Task<Result> CreateAsync(CreateOrderRequest request);
    Task<Result> UpdateAsync(Guid id, UpdateOrderRequest request);
    Task<Result> DeleteAsync(Guid id);
}