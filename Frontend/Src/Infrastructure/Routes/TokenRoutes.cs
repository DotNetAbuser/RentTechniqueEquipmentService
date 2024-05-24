namespace Infrastructure.Routes;

public static class TokenRoutes
{
    private const string BaseUrl = "api/identity/token";

    public static string SignIn => BaseUrl;
    public static string RefreshToken => BaseUrl + "/refresh-token";
}