namespace Server.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static void AddSwagger(this IApplicationBuilder application) =>
        application
            .UseSwagger()
            .UseSwaggerUI();
}