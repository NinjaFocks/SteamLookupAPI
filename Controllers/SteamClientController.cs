using Microsoft.AspNetCore.Mvc;
using SteamLookupAPI.SteamController;

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
    public async Task<string> GetUserStatus([FromQuery]ulong userId)
    {
        return await _steamFactory.GetUserStatus(userId);
    }

    [HttpGet("/createdDate")]
    [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "*" })]
    public async Task<DateTime> GetUserCreatedDate([FromQuery]ulong userId)
    {
        var result = await _steamFactory.GetUserCreatedDate(userId);

        return result;
    }
}
