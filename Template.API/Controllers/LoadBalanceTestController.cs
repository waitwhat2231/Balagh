using Microsoft.AspNetCore.Mvc;

namespace Template.API.Controllers;

[ApiController]
[Route("loadBalancingTest")]
public class LoadBalanceTestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var instance = Environment.GetEnvironmentVariable("INSTANCE_ID") ?? "unknown";
        var host = HttpContext.Request.Host.Value;
        return Ok(new
        {
            Instance = instance,
            Host = host,
            Time = DateTime.UtcNow,
            Message = $"Handled by {instance}"
        });
    }
}
