using Microsoft.AspNetCore.Mvc;

namespace Template.API.Controllers;

[ApiController]

public class LoadBalanceTestController : ControllerBase
{
    [HttpGet]
    [Route("LoadBalanceTest")]
    public IActionResult Get()
    {
        var instance = Environment.GetEnvironmentVariable("INSTANCE_ID") ?? "unknown";
        return Ok(new
        {
            Instance = instance,
            Time = DateTime.UtcNow,
            Message = $"Handled by {instance}"
        });
    }
}
