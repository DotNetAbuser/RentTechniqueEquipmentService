namespace Infrastructure.Managers;

public interface IOrderStatusManager
{
    Task<IResult<IEnumerable<OrderStatusResponse>>> GetAllAsync();
    
    Task<IResult<OrderStatusResponse>> GetByIdAsync(int id);
    
    Task<IResult> CreateAsync(CreateOrderStatusRequest request);
    Task<IResult> UpdateAsync(int id, UpdateOrderStatusRequest request);
    Task<IResult> DeleteAsync(int id);
}

public class OrderStatusManager(
    IHttpClientFactory factory)
    : IOrderStatusManager
{
    public async Task<IResult<IEnumerable<OrderStatusResponse>>> GetAllAsync()
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .GetAsync(OrderStatusRoutes.GetAll);
        return await response.ToResultAsync<IEnumerable<OrderStatusResponse>>();
    }

    public async Task<IResult<OrderStatusResponse>> GetByIdAsync(int id)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .GetAsync(OrderStatusRoutes.GetById(id));
        return await response.ToResultAsync<OrderStatusResponse>();
    }

    public async Task<IResult> CreateAsync(CreateOrderStatusRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PostAsJsonAsync(OrderRoutes.Create, request);
        return await response.ToResultAsync();
    }

    public async Task<IResult> UpdateAsync(int id, UpdateOrderStatusRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PutAsJsonAsync(OrderStatusRoutes.Update(id), request);
        return await response.ToResultAsync();
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .DeleteAsync(OrderStatusRoutes.Delete(id));
        return await response.ToResultAsync();
    }
}