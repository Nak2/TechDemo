using MySqlConnector;
using Server.Database.Builder;

namespace Server.Extensions;

public static class IConfigurationManagerExtension
{
    /// <summary>
    /// Builds the database if it does not exist.
    /// </summary>
    /// <param name="manager"></param>
    /// <returns>The connection string.</returns>
    public static void EnsureDatabase(this IConfigurationManager manager)
    {
        // Get the connection string and database name
        var conStringBuilder = new MySqlConnectionStringBuilder(manager.GetConnectionString("local")!);
        var dataBase = conStringBuilder.Database;
        conStringBuilder.Database = null;

        // Check if the database exists
        using var connection = new MySqlConnection(conStringBuilder.ToString());
        connection.Open();
        if (DatabaseExists(connection, dataBase))
            return;

        // Create the database and switch to it
        using var command = connection.CreateCommand();
        command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{dataBase}`;";
        command.ExecuteNonQuery();
        command.CommandText = $"USE `{dataBase}`;";
        command.ExecuteNonQuery();

        // List all IBuilder implementations
        var builders = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IBuilder).IsAssignableFrom(p) && !p.IsInterface)
            .Select(x => (IBuilder)Activator.CreateInstance(x)!)
            .ToList();

        // Create tables
        foreach (var builder in builders)
        {
            builder.CreateTable(connection);
        }

        // Add constraints
        foreach (var builder in builders)
        {
            builder.AddConstraints(connection);
        }
        connection.Close();
    }

    /// <summary>
    /// Checks if the database exists.
    /// </summary>
    /// <param name="connection">The MySQL connection.</param>
    /// <param name="databaseName">The name of the database.</param>
    /// <returns></returns>
    private static bool DatabaseExists(MySqlConnection connection, string databaseName)
    {
        using var command = connection.CreateCommand();

        command.CommandText = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}';";
        var existingDatabase = command.ExecuteScalar();
        return existingDatabase != null;
    }
}