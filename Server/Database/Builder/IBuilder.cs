using MySqlConnector;

namespace Server.Database.Builder;

/// <summary>
/// An interface for a MySQL database builder.
/// </summary>
public interface IBuilder
{
    /// <summary>
    /// Builds the database.
    /// </summary>
    /// <param name="mySql">The MySQL connection.</param>
    public void CreateTable(MySqlConnection mySql);

    /// <summary>
    /// Adds constraints to the database.
    /// </summary>
    /// <param name="mySql"></param>
    public void AddConstraints(MySqlConnection mySql);
}
