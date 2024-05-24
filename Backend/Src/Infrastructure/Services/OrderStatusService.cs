namespace Infrastructure.Services;

public class OrderStatusService(
    IOrderStatusRepository orderStatusRepository)
    : IOrderStatusService
{
    public async Task<Result<IEnumerable<OrderStatusResponse>>> GetAllAsync()
    {
        var orderStatusesEntities = await orderStatusRepository.GetAllAsync();
        var orderStatusesResponse =
            orderStatusesEntities.Select(orderStatusEntity => new OrderStatusResponse(
                Id: orderStatusEntity.Id,
                Name: orderStatusEntity.Name)).ToList();
        return Result<IEnumerable<OrderStatusResponse>>.Success(orderStatusesResponse, "Список статусов успешно получены.");
    }

    public async Task<Result<OrderStatusResponse>> GetByIdAsync(int id)
    {
        var orderStatusEntity = await orderStatusRepository.GetByIdAsync(id);
        if (orderStatusEntity == null)
            return Result<OrderStatusResponse>.Fail("Статус с данным идентификатором не найден!");

        var orderStatusResponse = new OrderStatusResponse(
            Id: orderStatusEntity.Id,
            Name: orderStatusEntity.Name);
        return Result<OrderStatusResponse>.Success(orderStatusResponse,"Статус успешно получен.");
    }

    public async Task<Result> CreateAsync(CreateOrderStatusRequest request)
    {
        var isExistByName = await orderStatusRepository.IsExistByNameAsync(request.Name);
        if (isExistByName)
            return Result<string>.Fail("Статус с данным названием уже сущетсвует в системе!");

        var orderStatusEntity = new OrderStatusEntity {
            Name = request.Name
        };
        await orderStatusRepository.CreateAsync(orderStatusEntity);
        return Result<string>.Success("Статус успешно создан.");
    }

    public async Task<Result> UpdateAsync(int id, UpdateOrderStatusRequest request)
    {
        var isExistForUpdateByName = await orderStatusRepository
            .IsExistForUpdateByName(id, request.Name);
        if (isExistForUpdateByName)
            return Result<string>.Fail("Статус с данным названием уже сущетсвует в системе!");

        var orderStatusEntity = await orderStatusRepository.GetByIdAsync(id);
        if (orderStatusEntity == null)
            return Result<string>.Fail("Статус с данным идентификатором не найден!");

        orderStatusEntity.Name = request.Name;
        await orderStatusRepository.UpdateAsync(orderStatusEntity);
        return Result<string>.Success("Статус успешно обновлён.");
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var orderStatusEntity = await orderStatusRepository.GetByIdAsync(id);
        if (orderStatusEntity == null)
            return Result<string>.Fail("Статус с данным идентификатором не найден!");

        await orderStatusRepository.DeleteAsync(orderStatusEntity);
        return Result<string>.Success("Статус успешно удалён.");
    }
}