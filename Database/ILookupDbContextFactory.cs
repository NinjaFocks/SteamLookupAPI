namespace SteamLookupAPI.Database;

public interface ILookupDbContextFactory
{
    Task UpdateUserRecordAsync(ulong steamKey, string? status = null, DateTime? createdDate = null);
}
