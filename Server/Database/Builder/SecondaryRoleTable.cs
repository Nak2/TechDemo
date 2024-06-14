using MySqlConnector;

namespace Server.Database.Builder;

public class SecondaryRoleTable : IBuilder
{
    /// <summary>
    /// Builds the secondary_role table.
    /// </summary>
    /// <param name="connection"></param>
    public void CreateTable(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS `secondary_role` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `user_id` INT,
                `district_id` INT
            );
        ";
        command.ExecuteNonQuery();
    }

    public void AddConstraints(MySqlConnection mySql)
    {
        using var command = mySql.CreateCommand();
        command.CommandText = @"
            ALTER TABLE `secondary_role`
            ADD CONSTRAINT `fk_secondary_user`
            FOREIGN KEY (`user_id`) REFERENCES `sales_user`(`id`) ON DELETE CASCADE;
        ";
        command.ExecuteNonQuery();

        command.CommandText = @"
            ALTER TABLE `secondary_role`
            ADD CONSTRAINT `fk_secondary_district`
            FOREIGN KEY (`district_id`) REFERENCES `districts`(`id`) ON DELETE CASCADE;
        ";
        command.ExecuteNonQuery();
    }
}
