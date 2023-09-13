using Microsoft.EntityFrameworkCore;
using SteamLookupAPI.Model;
using System.Text.Json;

namespace SteamLookupAPI.Middleware;

public class SaveQueryMiddleware
{
    private readonly RequestDelegate _next;
    private LookupDbContext _storageContext;

    public SaveQueryMiddleware(RequestDelegate next, IDbContextFactory<LookupDbContext> dbContext)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _storageContext = dbContext.CreateDbContext();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var query = context.Request.Query;

        var addedQuery = new Query
        {
            Parameters = SerialiseParams(query),
            Requested = DateTime.UtcNow,
            Path = context.Request.Path
        };

        _storageContext.Queries.Add(addedQuery);

        await _storageContext.SaveChangesAsync();

        await _next(context);
        return;
    }

    private string SerialiseParams(IQueryCollection query)
    {
        var result = JsonSerializer.Serialize(query);

        return result;
    }
}
