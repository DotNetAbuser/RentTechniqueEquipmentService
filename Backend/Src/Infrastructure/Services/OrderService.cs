namespace Infrastructure.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IEquipmentRepository equipmentRepository)
    : IOrderService
{
    public async Task<Result<PaginatedData<OrderResponse>>> GetPaginatedOrdersAsync(
        int pageNumber, int pageSize, string? searchTerms)
    {
        var (ordersEntities, totalCount) = await orderRepository.GetPaginatedOrdersWithIncludesAsync(
            pageNumber, pageSize, searchTerms);
        var ordersResponse = ordersEntities.Select(orderEntity => new OrderResponse(
            LastName: orderEntity.User.LastName,
            FirstName: orderEntity.User.FirstName,
            MiddleName: orderEntity.User.MiddleName,
            email: orderEntity.User.Email,
            phoneNumber: orderEntity.User.PhoneNumber,
            EquipmentName: orderEntity.Equipment.Name,
            StatusName: orderEntity.Status.Name,
            TotalCost: orderEntity.TotalCost,
            IsPayed: orderEntity.IsPayed,
            Arrived: orderEntity.Arrived,
            Created: orderEntity.Created));
        var paginatedOrdersResponse = new PaginatedData<OrderResponse>(
            List: ordersResponse, TotalCount: totalCount);
        return Result<PaginatedData<OrderResponse>>.Success(paginatedOrdersResponse, "Список заказов на аренду оборудования успешно получен.");
    }

    public async Task<Result<OrderResponse>> GetByIdAsync(Guid id)
    {
        var orderEntity = await orderRepository.GetByIdWithIncludesAsync(id);
        if (orderEntity == null)
            return Result<OrderResponse>.Fail("Заказ с данным идентификатором не найден!");

        var orderResponse = new OrderResponse(
            LastName: orderEntity.User.LastName,
            FirstName: orderEntity.User.FirstName,
            MiddleName: orderEntity.User.MiddleName,
            email: orderEntity.User.Email,
            phoneNumber: orderEntity.User.PhoneNumber,
            EquipmentName: orderEntity.Equipment.Name,
            StatusName: orderEntity.Status.Name,
            TotalCost: orderEntity.TotalCost,
            IsPayed: orderEntity.IsPayed,
            Arrived: orderEntity.Arrived,
            Created: orderEntity.Created);
        return Result<OrderResponse>.Success(orderResponse, "Заказ успешно получен.");
    }

    public async Task<Result> CreateAsync(CreateOrderRequest request)
    {
        var equipmentEntity = await equipmentRepository.GetByIdAsync(request.EquipmentId);
        if (equipmentEntity == null)
            return Result<string>.Fail("Тип оборудования с данным идентификатором не найден!");

        var totalCost = equipmentEntity.CostOneHour * request.CountRentHours;
        
        var orderEntity = new OrderEntity {
            UserId = request.UserId,
            EquipmentId = request.EquipmentId,
            StatusId = 1,
            CountRentHours = request.CountRentHours,
            TotalCost = totalCost,
            Arrived = request.Arrived.ToUniversalTime()
        };
        await orderRepository.CreateAsync(orderEntity);
        return Result<string>.Success("Заказ успешно создан.");
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateOrderRequest request)
    {
        var orderEntity = await orderRepository.GetByIdAsync(id);
        if (orderEntity == null)
            return Result<OrderResponse>.Fail("Заказ с данным идентификатором не найден!");
        
        var equipmentEntity = await equipmentRepository.GetByIdAsync(request.EquipmentId);
        if (equipmentEntity == null)
            return Result<string>.Fail("Тип оборудования с данным идентификатором не найден!");

        var totalCost = request.CountRentHours * equipmentEntity.CostOneHour;

        orderEntity.EquipmentId = request.EquipmentId;
        orderEntity.CountRentHours = request.CountRentHours;
        orderEntity.TotalCost = totalCost;
        orderEntity.IsPayed = false;
        orderEntity.Arrived = request.Arrived.ToUniversalTime();
        await orderRepository.UpdateAsync(orderEntity);
        return Result<string>.Success("Заказ успешно обновлён.");
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var orderEntity = await orderRepository.GetByIdAsync(id);
        if (orderEntity == null)
            return Result<OrderResponse>.Fail("Заказ с данным идентификатором не найден!");

        await orderRepository.DeleteAsync(orderEntity);
        return Result<string>.Success("Заказ успешно удалён");
    }
}