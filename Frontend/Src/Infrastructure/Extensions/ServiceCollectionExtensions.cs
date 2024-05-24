namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddManagers(this IServiceCollection services) =>
        services
            .AddTransient<IEquipmentManager, EquipmentManager>()
            .AddTransient<IOrderManager, OrderManager>()
            .AddTransient<IOrderStatusManager, OrderStatusManager>()
            .AddTransient<ITokenManager, TokenManager>()
            .AddTransient<IUserManager, UserManager>();

    public static void AddServices(this IServiceCollection services) =>
        services
            .AddTransient<ITokenService, TokenService>();

    public static void AddAndConfigureHttpClientFactory(this IServiceCollection services) =>
        services
            .AddTransient<AuthorizationHeaderHandler>()
            .AddHttpClient(ApplicationConstants.BaseClientName)
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(ApplicationConstants.BackendAddress))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();
}