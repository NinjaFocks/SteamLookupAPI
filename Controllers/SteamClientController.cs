using Microsoft.AspNetCore.Mvc;
using SteamLookupAPI.Steam;

namespace SteamLookupAPI.Controllers;

[ApiController]
[Route("user")]
public class SteamClientController : Controller
{
    private readonly ISteamUserInterfaceProcessor _steamUserProcessor;
    
    public SteamClientController(ISteamUserInterfaceProcessor steamUserProcessor)
    {
        _steamUserProcessor = steamUserProcessor;        
    }

    [HttpGet("/status")]
    [ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "*" })]
    public async Task<string> GetUserStatus([FromQuery] UserQuery query)
    {
        return await _steamUserProcessor.GetUserStatusAsync(query.UserId);
    }

    [HttpGet("/createdDate")]
    [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
    public async Task<DateTime> GetUserCreatedDate([FromQuery] UserQuery query)
    {
        return await _steamUserProcessor.GetUserCreatedDateAsync(query.UserId);
    }
}

public class UserQuery
{
    public ulong UserId { get; set; }
}