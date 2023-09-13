using System.ComponentModel.DataAnnotations;

namespace SteamLookupAPI.Model;

public class User
{
    [Key]
    public int UserId { get; set; }

    public ulong SteamKey { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }
}
