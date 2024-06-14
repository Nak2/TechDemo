namespace Server.Models;

/// <summary>
/// A class representing a secondary role.
/// </summary>
public class SecondaryRole
{
    /// <summary>
    /// The unique identifier for the secondary role.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The id of the sales user.
    /// </summary>
    public required int SalesUserId { get; set; }

    /// <summary>
    /// The id of the district.
    /// </summary>
    public required int DistrictId { get; set; }
}
