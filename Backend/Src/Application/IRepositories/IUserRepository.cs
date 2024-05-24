namespace Application.IRepositories;

public interface IUserRepository
{
    Task<UserEntity?> GetByPhoneNumberWithRoleAsync(string phoneNumber);
    Task<UserEntity?> GetByIdWithRoleAsync(Guid id);

    
    Task CreateAsync(UserEntity entity);
    
    Task<bool> IsExistByEmailAsync(string email);
    Task<bool> IsExistByPhoneNumberAsync(string phoneNumber);
}