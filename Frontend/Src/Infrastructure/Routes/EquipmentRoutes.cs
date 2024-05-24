namespace Infrastructure.Routes;

public static class EquipmentRoutes
{
    private const string BaseUrl = "api/order/equipment";

    public static string GetAll => BaseUrl;
    
    public static string GetById(int id) =>
         BaseUrl + $"/{id}";

    public static string Create => BaseUrl;

    public static string Update(int id) =>
        BaseUrl + $"/{id}";

    public static string Delete(int id) =>
        BaseUrl + $"/{id}";
}