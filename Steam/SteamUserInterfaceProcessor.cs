using SteamLookupAPI.Database;
using SteamLookupAPI.SteamController;
using SteamWebAPI2.Interfaces;

namespace SteamLookupAPI.Steam;

public class SteamUserInterfaceProcessor : ISteamUserInterfaceProcessor
{
    private readonly SteamUser _steamUserInterface;
    private readonly ILookupDbContextFactory _dbContext;

    public SteamUserInterfaceProcessor(ISteamFactory steamFactory, ILookupDbContextFactory dbContext)
    {
        _steamUserInterface = steamFactory.GetSteamUserInterface();

        _dbContext = dbContext;
    }

    public async Task<string> GetUserStatusAsync(ulong userId)
    {
        var result = await _steamUserInterface.GetPlayerSummaryAsync(userId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Could not find Player Summary for user with Key {userId}");
        }

        var status = result.Data.UserStatus.ToString();

        await _dbContext.UpdateUserRecordAsync(userId, status: status);

        return status;
    }

    public async Task<DateTime> GetUserCreatedDateAsync(ulong userId)
    {
        var result = await _steamUserInterface.GetPlayerSummaryAsync(userId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Could not find Player Summary for user with Key {userId}");
        }

        var createdDate = result.Data.AccountCreatedDate;

        await _dbContext.UpdateUserRecordAsync(userId, createdDate: createdDate);

        return createdDate;
    }
}
