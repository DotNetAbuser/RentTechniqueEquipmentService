namespace Infrastructure.Managers;

public interface IOrderManager
{
    Task<IResult<PaginatedData<OrderResponse>>> GetPaginatedOrdersAsync(
        int pageNumber, int pageSize, string? searchTerms);
    Task<IResult<OrderResponse>> GetByIdAsync(Guid id);
    Task<IResult> CreateAsync(CreateOrderRequest request);
    Task<IResult> UpdateAsync(Guid id, UpdateOrderRequest request);
    Task<IResult> DeleteAsync(Guid id);
}

public class OrderManager(
    IHttpClientFactory factory)
    : IOrderManager
{
    public async Task<IResult<PaginatedData<OrderResponse>>> GetPaginatedOrdersAsync(
        int pageNumber, int pageSize, string? searchTerms)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .GetAsync(OrderRoutes.GetPaginatedOrders(
                pageNumber, pageSize, searchTerms));
        return await response.ToResultAsync<PaginatedData<OrderResponse>>();
    }

    public async Task<IResult<OrderResponse>> GetByIdAsync(Guid id)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .GetAsync(OrderRoutes.GetById(id));
        return await response.ToResultAsync<OrderResponse>();
    }

    public async Task<IResult> CreateAsync(CreateOrderRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PostAsJsonAsync(OrderRoutes.Create, request);
        return await response.ToResultAsync();
    }

    public async Task<IResult> UpdateAsync(Guid id, UpdateOrderRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PutAsJsonAsync(OrderRoutes.Update(id), request);
        return await response.ToResultAsync();
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .DeleteAsync(OrderRoutes.Delete(id));
        return await response.ToResultAsync();
    }
}