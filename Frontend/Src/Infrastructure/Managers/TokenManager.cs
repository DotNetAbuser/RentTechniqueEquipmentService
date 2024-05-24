namespace Infrastructure.Managers;

public interface ITokenManager
{
    Task<IResult> SignInAsync(SignInRequest request);
    Task<IResult> RefreshTokenAsync(RefreshTokenRequest request);
}

public class TokenManager(
    ITokenService tokenService,
    IHttpClientFactory factory)
    : ITokenManager
{
    public async Task<IResult> SignInAsync(SignInRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PostAsJsonAsync(TokenRoutes.SignIn, request);
        var result = await response.ToResultAsync<SignInResponse>();
        if (!result.Succeeded)
            return Result.Fail(result.Messages);
        await tokenService.SetAuthTokenAsync(result.Data.AuthToken);
        await tokenService.SetRefreshTokenAsync(result.Data.RefreshToken);
        return Result.Success();
    }

    public async Task<IResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var response = await factory.CreateClient(ApplicationConstants.BaseClientName)
            .PostAsJsonAsync(TokenRoutes.RefreshToken, request);
        var result = await response.ToResultAsync<SignInResponse>();
        if (!result.Succeeded)
            return Result.Fail();
        await tokenService.SetAuthTokenAsync(result.Data.AuthToken);
        await tokenService.SetRefreshTokenAsync(result.Data.RefreshToken);
        return Result.Success();
    }
}