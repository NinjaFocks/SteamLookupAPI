using Microsoft.Extensions.Options;
using SteamLookupAPI.Config;
using SteamLookupAPI.Database;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;

namespace SteamLookupAPI.SteamController;

public class SteamFactory : ISteamFactory
{
    private readonly SteamUser _steamUserInfo;
    private readonly ILookupDbContextFactory _dbContext;

    public SteamFactory(IOptions<SteamConfig> steamConfigOptions, ILookupDbContextFactory dbContext)
    {
        var steamKey = steamConfigOptions.Value.SteamWebApiKey;

        var factory = new SteamWebInterfaceFactory(steamKey);

        _steamUserInfo = factory.CreateSteamWebInterface<SteamUser>();
        
        _dbContext = dbContext;
    }

    public async Task<string> GetUserStatusAsync(ulong userId)
    {        
        var result = await _steamUserInfo.GetPlayerSummaryAsync(userId);

        var status = result.Data.UserStatus.ToString();

        await _dbContext.UpdateUserRecordAsync(userId, status: status);

        return status;
    }

    public async Task<DateTime> GetUserCreatedDateAsync(ulong userId)
    {
        var result = await _steamUserInfo.GetPlayerSummaryAsync(userId);
        var createdDate = result.Data.AccountCreatedDate;

        await _dbContext.UpdateUserRecordAsync(userId, createdDate: createdDate);

        return createdDate;
    }    
}
