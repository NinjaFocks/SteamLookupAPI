using Microsoft.EntityFrameworkCore;
using SteamLookupAPI.Model;

namespace SteamLookupAPI.Database
{
    public class LookupDbContextFactory : ILookupDbContextFactory
    {
        private readonly LookupDbContext _storageContext;

        public LookupDbContextFactory(IDbContextFactory<LookupDbContext> dbContext)
        {
            _storageContext = dbContext.CreateDbContext();
        }

        public async Task UpdateUserRecordAsync(ulong steamKey, string? status = null, DateTime? createdDate = null)
        {
            var user = await _storageContext.Users.SingleOrDefaultAsync(u => u.SteamKey == steamKey);

            if (user == null)
            {
                user = new User
                {
                    SteamKey = steamKey,
                    Status = status,
                    CreatedDate = createdDate
                };

                _storageContext.Users.Add(user);
            }
            else
            {
                user.Status = status ?? user.Status;
                user.CreatedDate = createdDate ?? user.CreatedDate;

                _storageContext.Update(user);
            }            

            await _storageContext.SaveChangesAsync();
        }
    }
}
