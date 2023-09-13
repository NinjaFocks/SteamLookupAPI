namespace SteamLookupAPI.SteamController;

public interface ISteamFactory
{
    Task<string> GetUserStatus(ulong userId);

    Task<DateTime> GetUserCreatedDate(ulong userId);
}
