using Newtonsoft.Json;

namespace API.Helpers.Program;

public static class Plugins
{
    public static WebApplicationBuilder AddPlugins(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddCors()
            .AddDependencies();
        return builder;
    }

    public static WebApplication EnablePlugins(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(Cors.Name);
        return app;
    }
}