using MySqlConnector;

namespace Server.Database.Builder;

public class SalesUserTable : IBuilder
{
    /// <summary>
    /// Builds the sales user table.
    /// </summary>
    /// <param name="connection"></param>
    public void CreateTable(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS `sales_user` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `name` VARCHAR(255) NOT NULL UNIQUE,
                `district_id` INT
            );
        ";
        command.ExecuteNonQuery();
    }

    public void AddConstraints(MySqlConnection mySql)
    {
        using var command = mySql.CreateCommand();
        command.CommandText = @"
            ALTER TABLE `sales_user`
            ADD CONSTRAINT `fk_district_id`
            FOREIGN KEY (`district_id`)
            REFERENCES `districts`(`id`)
            ON DELETE RESTRICT;
        ";
        command.ExecuteNonQuery();
    }
}
