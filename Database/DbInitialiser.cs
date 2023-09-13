using Microsoft.Extensions.Options;
using SteamLookupAPI.Config;

namespace SteamLookupAPI.Model;

public class DbInitialiser
{
    static DbInitialiser()
    {          
    }

    public static void Initialize(LookupDbContext context)
    {
        context.Database.EnsureCreated();
                
        if (context.Users.Any())
        {
            return;   // DB has been created
        }

        var user = new User
        {
            SteamKey = 2
        };

        context.Users.Add(user);

        context.SaveChanges();
    }
}
