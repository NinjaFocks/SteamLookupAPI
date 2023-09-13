namespace SteamLookupAPI.SteamController;

public interface ISteamFactory
{
    Task<string> GetUserStatusAsync(ulong userId);

    Task<DateTime> GetUserCreatedDateAsync(ulong userId);
}
