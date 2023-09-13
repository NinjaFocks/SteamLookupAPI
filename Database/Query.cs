using Microsoft.EntityFrameworkCore;

namespace SteamLookupAPI.Model;

public class Query
{
    public int Id { get; set; }

    public string Path { get; set; }

    public string? Parameters { get; set; }

    public DateTime Requested { get; set; }
}
