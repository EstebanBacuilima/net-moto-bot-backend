using Microsoft.AspNetCore.Mvc;

namespace net_moto_bot.API.Controllers.V1.Test;

[Route("api/v1/test")]
[ApiController]
//[Authorize]
public class TestController() : CommonController
{
    [HttpGet, Route("run")]
    public IActionResult GetRun()
    {
        return Ok("Service Running...");
    }
}