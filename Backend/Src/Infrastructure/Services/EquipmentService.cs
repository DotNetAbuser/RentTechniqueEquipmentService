namespace Infrastructure.Services;

public class EquipmentService(
    IEquipmentRepository equipmentRepository,
    IUploadFileHelper uploadFileHelper)
    : IEquipmentService
{
    public async Task<Result<IEnumerable<EquipmentResponse>>> GetAllAsync()
    {
        var equipmentsEntities = await equipmentRepository.GetAllAsync();
        var equipmentsResponse = equipmentsEntities
            .Select(equipmentEntity =>
                new EquipmentResponse(
                    Id: equipmentEntity.Id,
                    Name: equipmentEntity.Name,
                    Description: equipmentEntity.Description,
                    PicturePath: equipmentEntity.PicturePath,
                    CostOneHour: equipmentEntity.CostOneHour)).ToList();
        return Result<IEnumerable<EquipmentResponse>>.Success(equipmentsResponse, "Типы оборудования успешно получены.");
    }

    public async Task<Result<EquipmentResponse>> GetByIdAsync(int id)
    {
        var equipmentEntity = await equipmentRepository.GetByIdAsync(id);
        if (equipmentEntity == null)
            return Result<EquipmentResponse>.Fail("Тип оборудования с данным идентификатором не найден!");

        var equipmentResponse = new EquipmentResponse(
            Id: equipmentEntity.Id,
            Name: equipmentEntity.Name,
            Description: equipmentEntity.Description,
            PicturePath: equipmentEntity.PicturePath,
            CostOneHour: equipmentEntity.CostOneHour);
        return Result<EquipmentResponse>.Success(equipmentResponse, "Тип оборудования успешно получен.");
    }

    public async Task<Result> CreateAsync(CreateEquipmentRequest request)
    {
        var isExistByName = await equipmentRepository
            .IsExistByNameAsync(request.Name);
        if (isExistByName)
            return Result<string>.Fail("Тип оборудования с таким названием уже существует!");

        var isExistByDescription = await equipmentRepository
            .IsExistByDescriptionAsync(request.Description);
        if (isExistByDescription)
            return Result<string>.Fail("Тип оборудования с таким описанием уже существует!");

        var equipmentEntity = new EquipmentEntity {
            Name = request.Name,
            Description = request.Description,
            CostOneHour = request.CostOneHour
        };
        if (request.EquipmentPictureUploadRequest != null)
            equipmentEntity.PicturePath = uploadFileHelper.Upload(request.EquipmentPictureUploadRequest);
        await equipmentRepository.CreateAsync(equipmentEntity);
        return Result<string>.Success("Новый тип оборудования успешно создан.");
    }

    public async Task<Result> UpdateAsync(int id, UpdateEquipmentRequest request)
    {
        var isExistForUpdateByName = await equipmentRepository
            .IsExistForUpdateByName(id, request.Name);
        if (isExistForUpdateByName)
            return Result<string>.Fail("Тип оборудования с таким названием уже существует!");

        var isExistForUpdateByDescription = await equipmentRepository
            .IsExistForUpdateByDescription(id, request.Description);
        if (isExistForUpdateByDescription)
            return Result<string>.Fail("Тип оборудования с таким описанием уже существует!");

        var equipmentEntity = await equipmentRepository.GetByIdAsync(id);
        if (equipmentEntity == null)
            return Result<string>.Fail("Тип оборудования с данным идентификатором!");

        equipmentEntity.Name = request.Name;
        equipmentEntity.Description = request.Description;
        equipmentEntity.CostOneHour = request.CostOneHour;
        await equipmentRepository.UpdateAsync(equipmentEntity);
        return Result<string>.Success("Тип оборудования успешно обновлён.");
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var equipmentEntity = await equipmentRepository.GetByIdAsync(id);
        if (equipmentEntity == null)
            return Result<string>.Fail("Тип оборудования с данным идентификатором!");

        await equipmentRepository.DeleteAsync(equipmentEntity);
        return Result<string>.Success("Тип оборудования успешно удалён.");
    }
}