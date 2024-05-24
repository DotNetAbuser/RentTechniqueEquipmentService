namespace Application.IServices;

public interface ITokenService
{
    Task<Result<SignInResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<Result<SignInResponse>> SignInAsync(SignInRequest request);
}