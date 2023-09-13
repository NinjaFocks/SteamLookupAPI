using Microsoft.Extensions.Options;
using SteamLookupAPI.Config;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;

namespace SteamLookupAPI.SteamController;

public class SteamFactory : ISteamFactory
{
    private readonly SteamWebInterfaceFactory _steamInterfaceFactory;

    public SteamFactory(IOptions<SteamConfig> steamConfigOptions)
    {
        var steamKey = steamConfigOptions.Value.SteamWebApiKey;

        _steamInterfaceFactory = new SteamWebInterfaceFactory(steamKey);                        
    }

    public SteamUser GetSteamUserInterface()
    {
        return _steamInterfaceFactory.CreateSteamWebInterface<SteamUser>();
    }   
}
