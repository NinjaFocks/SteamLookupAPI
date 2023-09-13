using SteamWebAPI2.Interfaces;

namespace SteamLookupAPI.SteamController;

public interface ISteamFactory
{
    SteamUser GetSteamUserInterface();
}
