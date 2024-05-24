namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddHelpers(this IServiceCollection services) =>
        services
            .AddTransient<IUploadFileHelper, UploadFileHelper>();
    
    public static void AddRepositories(this IServiceCollection services) =>
        services
            .AddTransient<IEquipmentRepository, EquipmentRepository>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IOrderStatusRepository, OrderStatusRepository>()
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<ISessionRepository, SessionRepository>()
            .AddTransient<IUserRepository, UserRepository>();

    public static void AddServices(this IServiceCollection services) =>
        services
            .AddTransient<IEquipmentService, EquipmentService>()
            .AddTransient<IOrderService, OrderService>()
            .AddTransient<IOrderStatusService, OrderStatusService>()
            .AddTransient<ITokenService, TokenService>()
            .AddTransient<IUserService, UserService>();
}