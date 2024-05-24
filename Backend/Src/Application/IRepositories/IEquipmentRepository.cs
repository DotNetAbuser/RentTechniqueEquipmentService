namespace Application.IRepositories;

public interface IEquipmentRepository
{
    Task<IEnumerable<EquipmentEntity>> GetAllAsync();
    Task<EquipmentEntity?> GetByIdAsync(int id);

    Task CreateAsync(EquipmentEntity entity);
    Task UpdateAsync(EquipmentEntity entity);
    Task DeleteAsync(EquipmentEntity entity);

    
    Task<bool> IsExistByNameAsync(string name);
    Task<bool> IsExistByDescriptionAsync(string description);
    Task<bool> IsExistForUpdateByName(int id, string name);
    Task<bool> IsExistForUpdateByDescription(int id, string description);
}