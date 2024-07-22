namespace API.Helpers.Program;

public static class Cors
{
    public const string Name = "_myAllowSpecificOrigins";

    public static IServiceCollection AddCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options => options.AddPolicy(name: Name,
            conf =>
            {
                conf.WithOrigins(
                        "http://localhost:3000"
                    )
                    .WithMethods("GET", "PUT", "DELETE", "POST", "get", "put", "delete", "post")
                    .AllowAnyHeader();
            }));
        return serviceCollection;
    }
}