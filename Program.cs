using SteamLookupAPI.Config;
using SteamLookupAPI.SteamController;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ISteamFactory, SteamFactory>();

        builder.Services.Configure<SteamConfig>(builder.Configuration.GetSection("SteamConfig"));
        builder.Services.Configure<ElasticConfig>(builder.Configuration.GetSection("ElasticConfig"));

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

        app.Run();
    }
}