using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;

namespace Server.HealthChecks;

public class DatabaseHealthCheck(IConfiguration configuration) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = false;

        // Check if the database is connected.
        try
        {
            using var connection = new MySqlConnection(configuration.GetConnectionString("local"));
            connection.Open();
            isHealthy = true;
            connection.Close();
        }
        catch (MySqlException)
        {
            isHealthy = false;
        }
        

        if (isHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "Unable to connect to the database."));
    }
}
