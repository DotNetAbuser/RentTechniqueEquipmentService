namespace Application.IServices;

public interface IUserService
{
    Task<Result> CreateAsync(SignUpRequest request);
}