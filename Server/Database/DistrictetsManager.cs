using MySqlConnector;
using Server.Model;
using Server.Models;

namespace Server.Database;

/// <summary>
/// A class for interacting with the Districtet table in the database.
/// </summary>
/// <param name="configuration"></param>
public class DistrictetsManager(IConfiguration configuration) : IDistrictetsManager
{
    private protected readonly string _connectionString = configuration.GetConnectionString("local")!;

    /// <summary>
    /// Adds a new districtet to the database.
    /// </summary>
    /// <param name="primaryUserId">The primary user of the districtet.</param>
    /// <param name="name">The name of the districtet.</param>
    /// <returns>The id of the new districtet.</returns>
    public OperationResult<int> AddDistrictet(int primaryUserId, string name)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "INSERT INTO districts (name, primary_user_id) VALUES (@Name, @PrimaryUserId);";
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@PrimaryUserId", primaryUserId);
            cmd.ExecuteNonQuery();

            return new OperationResult<int>(true, (int)cmd.LastInsertedId);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<int>(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Returns a list of all districtets in the database.
    /// </summary>
    /// <returns>The list of districtets.</returns>
    /// <exception cref="MySqlException">Throws when an error occurs with the MySQL database.</exception>
    public OperationResult<List<Districtet>> GetAll()
    {
        try
        {
            var districtets = new List<Districtet>();
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM districts;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                districtets.Add(new Districtet
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    PrimaryUserId = reader.GetInt32("primary_user_id")
                });
            }
            con.Close();

            return new OperationResult<List<Districtet>>(true, districtets);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<List<Districtet>>(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Returns the districtet with the given id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OperationResult<Districtet?> Get(int id)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM districts WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return new OperationResult<Districtet?>(true, null);

            var districtet = new Districtet
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                PrimaryUserId = reader.GetInt32("primary_user_id")
            };

            return new OperationResult<Districtet?>(true, districtet);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<Districtet?>(false)
            {
                Error = ex.Message
            };

            return result;
        }
    }

    /// <summary>
    /// Updates the districtet in the database.
    /// </summary>
    /// <param name="districtet"></param>
    public OperationResult Update(Districtet districtet)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "UPDATE districts SET name = @Name, primary_user_id = @PrimaryUserId WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Name", districtet.Name);
            cmd.Parameters.AddWithValue("@PrimaryUserId", districtet.PrimaryUserId);
            cmd.Parameters.AddWithValue("@Id", districtet.Id);
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
    /// Deletes the districtet with the given id from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if the districtet was deleted, false otherwise.</returns>
    public OperationResult Delete(int id)
    {
        try
        {
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "DELETE FROM districts WHERE id = @Id;";
            cmd.Parameters.AddWithValue("@Id", id);
            var found = cmd.ExecuteNonQuery() > 0;

            return new OperationResult(found);
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
    /// Returns a list of all salesusers for the given district.
    /// </summary>
    /// <param name="districtId"></param>
    /// <returns></returns>
    public OperationResult<List<SalesUser>> GetSalesUsers(int districtId)
    {
        try
        {
            var users = new List<SalesUser>();
            using var con = new MySqlConnection(_connectionString);
            using var cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "SELECT * FROM sales_user WHERE district_id = @DistrictId;";
            cmd.Parameters.AddWithValue("@DistrictId", districtId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new SalesUser
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    DistrictetId = reader.GetInt32("district_id")
                });
            }
            con.Close();
            return new OperationResult<List<SalesUser>>(true, users);
        }
        catch (MySqlException ex)
        {
            var result = new OperationResult<List<SalesUser>>(false)
            {
                Error = ex.Message
            };
            return result;
        }
    }
}
