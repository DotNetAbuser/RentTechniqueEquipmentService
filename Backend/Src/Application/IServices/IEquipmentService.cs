namespace Application.IServices;

public interface IEquipmentService
{
    Task<Result<IEnumerable<EquipmentResponse>>> GetAllAsync();
    Task<Result<EquipmentResponse>> GetByIdAsync(int id);
    Task<Result> CreateAsync(CreateEquipmentRequest request);
    Task<Result> UpdateAsync(int id, UpdateEquipmentRequest request);
    Task<Result> DeleteAsync(int id);
}