namespace Server.Model;

/// <summary>
/// A class representing a districtets.
/// </summary>
public class Districtet
{
    /// <summary>
    /// The unique identifier for the districtet.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the districtet.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The primary user of the districtet.
    /// </summary>
    public required int PrimaryUserId { get; set; }
}
