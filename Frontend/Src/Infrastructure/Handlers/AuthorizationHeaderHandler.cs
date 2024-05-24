namespace Infrastructure.Handlers;

public class AuthorizationHeaderHandler(
    ITokenManager tokenManager,
    ITokenService tokenService)
    : DelegatingHandler
{
    private bool isRefreshing;
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var authToken = await tokenService.GetAuthTokenAsync();

        if (!string.IsNullOrWhiteSpace(authToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        var response = await base.SendAsync(request, cancellationToken);
        if (isRefreshing || string.IsNullOrWhiteSpace(authToken) ||
            response.StatusCode != HttpStatusCode.Unauthorized)
            return response;
        try
        {
            isRefreshing = true;
            authToken = await tokenService.GetAuthTokenAsync();
            var refreshToken = await tokenService.GetRefreshTokenAsync();
            var refreshTokenRequest = new RefreshTokenRequest {
                AuthToken = authToken,
                RefreshToken = refreshToken
            };
            var result = await tokenManager.RefreshTokenAsync(refreshTokenRequest);
            if (result.Succeeded)
            {
                authToken = await tokenService.GetAuthTokenAsync();
                if (!string.IsNullOrWhiteSpace(authToken))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                response = await base.SendAsync(request, cancellationToken);
            }
        }
        finally
        {
            isRefreshing = false;
        }
        return response;
    }
}