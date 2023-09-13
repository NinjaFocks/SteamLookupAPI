namespace SteamLookupAPI.Steam;

public interface ISteamUserInterfaceProcessor
{
    Task<string> GetUserStatusAsync(ulong userId);

    Task<DateTime> GetUserCreatedDateAsync(ulong userId);
}
