using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamLookupAPI.Model;

public class LookupDbContext : DbContext
{
    public LookupDbContext(DbContextOptions<LookupDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Query> Queries { get; set; }
}
