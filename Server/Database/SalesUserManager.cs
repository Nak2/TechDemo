using MySqlConnector;
using Server.Models;
using System.Data;

namespace Server.Database;

/// <summary>
/// A controller for the user database.
/// </summary>
/// <param name="configuration"></param>
public class SalesUserManager(IConfiguration configuration, ILogger<SalesUserManager> logger) : ISalesUserManager
{
    private protected readonly string _connectionString = configuration.GetConnectionString("local")!;
    private protected readonly ILogger<SalesUserManager> _logger = logger;

    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="user">The user model</param>
    /// <returns>Returns the id of the new user. Otherwise, returns -1.</returns>
    public OperationResult<int> AddUser(SalesUser user)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "INSERT INTO sales_user (name, district_id) VALUES (@Name, @DistrictetId);";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@DistrictetId", user.DistrictetId);
            cmd.ExecuteNonQuery();

            return new OperationResult<int>(true, (int)cmd.LastInsertedId);
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, "An error occurred while adding a user.");

            var result = new OperationResult<int>(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }

    /// <summary>
    /// Returns a list of all users in the database.
    /// </summary>
    /// <returns></returns>
    public OperationResult<List<SalesUser>> GetUsers()
    {
        try
        {
            var users = new List<SalesUser>();
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM sales_user;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new SalesUser
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    DistrictetId = reader.IsDBNull("district_id") ? null : reader.GetInt32("district_id")
                });
            }

            return new OperationResult<List<SalesUser>>(true, users);
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, "An error occurred while getting users.");

            var result = new OperationResult<List<SalesUser>>(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }

    /// <summary>
    /// Returns a user with the given id.
    /// </summary>
    /// <param name="id">The id of the user to return.</param>
    /// <returns>The user with the given id, or null if no user is found.</returns>
    public OperationResult<SalesUser?> GetUser(int id)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM sales_user WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var user = new SalesUser
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    DistrictetId = reader.IsDBNull("district_id") ? null : reader.GetInt32("district_id")
                };
                return new OperationResult<SalesUser?>(true, user);
            }
            return new OperationResult<SalesUser?>(true, null);
        }
        catch (MySqlException ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while getting a user.");

            var result = new OperationResult<SalesUser?>(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }

    /// <summary>
    /// Updates the user in the database.
    /// </summary>
    /// <param name="user"></param>
    public OperationResult UpdateUser(SalesUser user)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "UPDATE sales_user SET name = @Name, district_id = @DistrictetId WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@DistrictetId", user.DistrictetId);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.ExecuteNonQuery();

            return new OperationResult(true);
        }
        catch (MySqlException ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while updating a user.");

            var result = new OperationResult(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }

    /// <summary>
    /// Deletes the user with the given id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns true if the user was deleted, false otherwise.</returns>
    public OperationResult<bool> DeleteUser(int id)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "DELETE FROM sales_user WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Id", id);

            return new OperationResult<bool>(true, cmd.ExecuteNonQuery() > 0);
        }
        catch (MySqlException ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while deleting a user.");

            var result = new OperationResult<bool>(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }
}
