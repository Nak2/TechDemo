using MySqlConnector;
using Server.Models;

namespace Server.Database;

public class SecondaryRoleManager(IConfiguration configuration) : ISecondaryRoleManager
{
    private protected readonly string _connectionString = configuration.GetConnectionString("local")!;

    /// <summary>
    /// Adds a new secondary role to the database.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="disctrictId"></param>
    public OperationResult Add(int userId, int disctrictId)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "INSERT INTO secondary_role (user_id, district_id) VALUES (@UserId, @DistrictId);";
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@DistrictId", disctrictId);
            cmd.ExecuteNonQuery();

            return new OperationResult(true);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Removes a secondary role from the database.
    /// </summary>
    /// <param name="userId">The id of the user to remove the secondary role from.</param>
    /// <param name="districtId">The id of the district to remove the secondary role from.</param>
    /// <exception cref="MySqlException">Throws when an error occurs with the MySQL database.</exception>
    public OperationResult Remove(int userId, int districtId)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "DELETE FROM secondary_role WHERE user_id = @UserId AND district_id = @DistrictId;";
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@DistrictId", districtId);
            cmd.ExecuteNonQuery();

            return new OperationResult(true);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Returns a list of all secondary roles in the database.
    /// </summary>
    /// <returns>A <see cref="List{T}"/> of <see cref="SecondaryRole"/>s.</returns>
    /// <exception cref="MySqlException">Throws when an error occurs with the MySQL database.</exception>
    public OperationResult<List<SecondaryRole>> GetAll()
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM secondary_role;";
            using var reader = cmd.ExecuteReader();
            var secondaryRoles = new List<SecondaryRole>();
            while (reader.Read())
            {
                secondaryRoles.Add(new SecondaryRole
                {
                    SalesUserId = reader.GetInt32("user_id"),
                    DistrictId = reader.GetInt32("district_id")
                });
            }
            return new OperationResult<List<SecondaryRole>>(true, secondaryRoles);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<List<SecondaryRole>>(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Returns a list of all secondary roles for a user.
    /// </summary>
    /// <param name="userId">The id of the user to return secondary roles for.</param>
    /// <returns>A <see cref="List{T}"/> of <see cref="SecondaryRole"/> objects.</returns>
    /// <exception cref="MySqlException">Throws when an error occurs with the MySQL database.</exception>
    public OperationResult<List<SecondaryRole>> GetUserRoles(int userId)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM secondary_role WHERE user_id = @UserId;";
            cmd.Parameters.AddWithValue("@UserId", userId);
            using var reader = cmd.ExecuteReader();
            var secondaryRoles = new List<SecondaryRole>();
            while (reader.Read())
            {
                secondaryRoles.Add(new SecondaryRole
                {
                    SalesUserId = reader.GetInt32("user_id"),
                    DistrictId = reader.GetInt32("district_id")
                });
            }

            return new OperationResult<List<SecondaryRole>>(true, secondaryRoles);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<List<SecondaryRole>>(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Returns a list of all user-ids with a secondary role in a district.
    /// </summary>
    /// <param name="districtId"></param>
    /// <returns>A <see cref="List{T}"/> of <see cref="int"/> user-ids.</returns>
    /// <exception cref="MySqlException">Throws when an error occurs with the MySQL database.</exception>
    public OperationResult<List<int>> GetSecondaryRoleUsers(int districtId)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT user_id FROM secondary_role WHERE district_id = @DistrictId;";
            cmd.Parameters.AddWithValue("@DistrictId", districtId);
            using var reader = cmd.ExecuteReader();
            var userIds = new List<int>();
            while (reader.Read())
            {
                userIds.Add(reader.GetInt32("user_id"));
            }

            return new OperationResult<List<int>>(true, userIds);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<List<int>>(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }
}
