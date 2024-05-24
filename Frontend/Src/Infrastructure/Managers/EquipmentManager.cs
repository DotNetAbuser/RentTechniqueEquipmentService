namespace Infrastructure.Managers;

public interface IEquipmentManager
{
    Task<IResult<IEnumerable<EquipmentResponse>>> GetAllAsync();
    Task<IResult<EquipmentResponse>> GetByIdAsync(int id);
    Task<IResult> CreateAsync(CreateEquipmentRequest request);
    Task<IResult> UpdateAsync(int id, UpdateEquipmentRequest request);
    Task<IResult> DeleteAsync(int id);  
}

public class EquipmentManager(
    IHttpClientFactory factory)
    : IEquipmentManager
{
    public async Task<IResult<IEnumerable<EquipmentResponse>>> GetAllAsync()
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .GetAsync(EquipmentRoutes.GetAll);
        return await response.ToResultAsync<IEnumerable<EquipmentResponse>>();
    }

    public async Task<IResult<EquipmentResponse>> GetByIdAsync(int id)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .GetAsync(EquipmentRoutes.GetById(id));
        return await response.ToResultAsync<EquipmentResponse>();
    }

    public async Task<IResult> CreateAsync(CreateEquipmentRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PostAsJsonAsync(EquipmentRoutes.Create, request);
        return await response.ToResultAsync();
    }

    public async Task<IResult> UpdateAsync(int id, UpdateEquipmentRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PutAsJsonAsync(EquipmentRoutes.Update(id), request);
        return await response.ToResultAsync();
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .DeleteAsync(EquipmentRoutes.Delete(id));
        return await response.ToResultAsync();
    }
}