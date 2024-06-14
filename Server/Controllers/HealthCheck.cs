using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController(HealthCheckService service) : ControllerBase
{
    private readonly HealthCheckService _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var report = await _service.CheckHealthAsync();
        string json = System.Text.Json.JsonSerializer.Serialize(report);

        if (report.Status == HealthStatus.Healthy)
            return Ok(json);
        return StatusCode(503, json);
    }
}
