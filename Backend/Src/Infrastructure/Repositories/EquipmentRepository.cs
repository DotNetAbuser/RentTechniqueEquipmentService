namespace Infrastructure.Repositories;

public class EquipmentRepository(
    ApplicationDbContext dbContext)
    : IEquipmentRepository
{
    public async Task<IEnumerable<EquipmentEntity>> GetAllAsync()
    {
        return await dbContext.Equipments
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EquipmentEntity?> GetByIdAsync(int id)
    {
        return await dbContext.Equipments
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(EquipmentEntity entity)
    {
        await dbContext.Equipments.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(EquipmentEntity entity)
    {
        dbContext.Equipments.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(EquipmentEntity entity)
    {
        dbContext.Equipments.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsExistByNameAsync(string name)
    {
        return await dbContext.Equipments
            .AsNoTracking()
            .AnyAsync(x => x.Name == name);
    }

    public async Task<bool> IsExistByDescriptionAsync(string description)
    {
        return await dbContext.Equipments
            .AsNoTracking()
            .AnyAsync(x => x.Description == description);
    }

    public async Task<bool> IsExistForUpdateByName(int id, string name)
    {
        return await dbContext.Equipments
            .AsNoTracking()
            .AnyAsync(x => x.Id != id && x.Name == name);
    }

    public async Task<bool> IsExistForUpdateByDescription(int id, string description)
    {
        return await dbContext.Equipments
            .AsNoTracking()
            .AnyAsync(x => x.Id != id && x.Description == description);
    }
}