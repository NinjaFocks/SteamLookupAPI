using Microsoft.AspNetCore.Mvc;
using SteamLookupAPI.SteamController;
using System.Text.Json;

namespace SteamLookupAPI.Controllers;

[ApiController]
[Route("user")]
public class SteamClientController : Controller
{
    private readonly ISteamFactory _steamFactory;
    
    public SteamClientController(ISteamFactory steamFactory)
    {
        _steamFactory = steamFactory;        
    }

    [HttpGet("/status")]
    [ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "*" })]
    public async Task<string> GetUserStatus([FromQuery] UserQuery query)
    {
        return await _steamFactory.GetUserStatusAsync(query.UserId);
    }

    [HttpGet("/createdDate")]
    [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
    public async Task<DateTime> GetUserCreatedDate([FromQuery] UserQuery query)
    {
        return await _steamFactory.GetUserCreatedDateAsync(query.UserId);
    }
}

public class UserQuery
{
    public ulong UserId { get; set; }
}