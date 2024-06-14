namespace Server.Models;

/// <summary>
/// A class representing a sales user.
/// </summary>
public class SalesUser
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? DistrictetId { get; set; }
}
