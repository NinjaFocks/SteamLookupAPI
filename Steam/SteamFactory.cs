using Microsoft.Extensions.Options;
using Nest;
using SteamLookupAPI.Config;
using SteamLookupAPI.Model;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System.Net;
using System.Security;

namespace SteamLookupAPI.SteamController;

public class SteamFactory : ISteamFactory
{
    private readonly SteamUser _steamUserInfo;
    private readonly ElasticClient _elastic;

    public SteamFactory(IOptions<SteamConfig> steamConfigOptions, 
        IOptions<ElasticConfig> elasticConfigOptions)
    {
        var steamKey = steamConfigOptions.Value.SteamWebApiKey;

        var factory = new SteamWebInterfaceFactory(steamKey);

        _steamUserInfo = factory.CreateSteamWebInterface<SteamUser>();

        //var secureString = new NetworkCredential("", elasticConfigOptions.Value.ApiKey).SecurePassword;
        var elasticSettings = new ConnectionSettings(new Uri(elasticConfigOptions.Value.ElasticUri))
            .DefaultIndex(elasticConfigOptions.Value.DefaultIndex)
            //.ApiKeyAuthentication("id", secureString)
            .PrettyJson();
            

        _elastic = new ElasticClient(elasticSettings);
    }

    public async Task<string> GetUserStatus(ulong userId)
    {
        var result = await _steamUserInfo.GetPlayerSummaryAsync(userId);

        var status = result.Data.UserStatus.ToString();

        UpdateUserRecord(userId, status);

        return status;
    }

    public async Task<DateTime> GetUserCreatedDate(ulong userId)
    {
        var result = await _steamUserInfo.GetPlayerSummaryAsync(userId);

        return result.Data.AccountCreatedDate;
    }

    private async void UpdateUserRecord(ulong userId, string? status = null, DateTime? createdDate = null)
    {
        var user = new User
        {
            Id = userId,
            Status = status,
            CreatedDate = createdDate
        };

        var indexResponse = await _elastic.IndexDocumentAsync(user);

        if (indexResponse != null && indexResponse.IsValid)
        {
            return;
        }
        else
        {
            throw new Exception("Failed to index record");
        }
    }
}
