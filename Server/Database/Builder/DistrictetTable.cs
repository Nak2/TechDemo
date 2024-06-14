using MySqlConnector;

namespace Server.Database.Builder;

public class DistrictetTable : IBuilder
{
    /// <summary>
    /// Builds the district table.
    /// </summary>
    /// <param name="connection"></param>
    public void CreateTable(MySqlConnection mySql)
    {
        using var command = mySql.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS `districts` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `name` VARCHAR(255) NOT NULL UNIQUE,
                `primary_user_id` INT
            );
        ";
        command.ExecuteNonQuery();
    }

    public void AddConstraints(MySqlConnection mySql)
    {
        using var command = mySql.CreateCommand();
        command.CommandText = @"
            ALTER TABLE `districts`
            ADD CONSTRAINT `fk_primary_user_id`
            FOREIGN KEY (`primary_user_id`)
            REFERENCES `sales_user`(`id`)
            ON DELETE RESTRICT;
        ";
        command.ExecuteNonQuery();
    }
}
