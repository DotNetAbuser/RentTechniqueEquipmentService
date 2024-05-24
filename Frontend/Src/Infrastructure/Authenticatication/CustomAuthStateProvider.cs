namespace Infrastructure.Authenticatication;

public class CustomAuthStateProvider(
    ITokenService tokenService) 
    : AuthenticationStateProvider
{
    public async Task StateChangedAsync()
    {
        var authState = Task.FromResult(await GetAuthenticationStateAsync());
        NotifyAuthenticationStateChanged(authState);
    }
    
    public async Task MarkUserAsLoggedOut()
    {
        await tokenService.RemoveAuthTokenAsync();
        await tokenService.RemoveRefreshTokenAsync();
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenService.GetAuthTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        Claim[] claims = [
            new Claim(ClaimTypes.NameIdentifier, tokenService.GetUserIdFromAuthToken(token)),
            new Claim(ClaimTypes.Role, tokenService.GetRoleFromAuthToken(token)),
        ];
        var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        return state;
    }
}