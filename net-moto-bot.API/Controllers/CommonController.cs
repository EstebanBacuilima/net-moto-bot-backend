using Microsoft.AspNetCore.Mvc;

namespace net_moto_bot.API.Controllers;

public class CommonController : ControllerBase
{
    /// <summary>
    /// Returns the current JWT Bearer for each request.
    /// </summary>
    protected string Token => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    protected string HostURL => $"{Request.Scheme}:://{Request.Host}";

    /// <summary>
    /// Get the user code that manage petition.
    /// </summary>
    protected string UserCode => Request.Headers["X-User-Code"].ToString();

    /// <summary>
    /// Get the remote ip address.
    /// </summary>
    protected string RemoteIpAddress
    {
        get
        {
            return Convert.ToString(HttpContext.Request.Headers["X-Forwarded-For"]) ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }
    }
}
