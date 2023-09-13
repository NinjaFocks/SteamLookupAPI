using Microsoft.EntityFrameworkCore;
using SteamLookupAPI.Config;
using SteamLookupAPI.Database;
using SteamLookupAPI.Middleware;
using SteamLookupAPI.Model;
using SteamLookupAPI.Steam;
using SteamLookupAPI.SteamController;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContextFactory<LookupDbContext>(options => 
            options.UseSqlServer("Data Source=DESKTOP-0ORVQ74;initial catalog=steam-lookup;integrated security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"));

        builder.Services.AddSingleton<ISteamFactory, SteamFactory>();
        builder.Services.AddSingleton<ILookupDbContextFactory, LookupDbContextFactory>();
        builder.Services.AddSingleton<ISteamUserInterfaceProcessor, SteamUserInterfaceProcessor>();
        
        builder.Services.Configure<SteamConfig>(builder.Configuration.GetSection("SteamConfig"));

        builder.Services.AddResponseCaching();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseResponseCaching();

        CreateDbIfNotExists(app);

        app.UseMiddleware<SaveQueryMiddleware>();

        app.Run();
    }

    private static void CreateDbIfNotExists(IApplicationBuilder appBuilder)
    {
        using (var scope = appBuilder.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<LookupDbContext>();
                DbInitialiser.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}