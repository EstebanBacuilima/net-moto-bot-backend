using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Dtos.Custom;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers;

[Route("api/v1/appointment")]
[ApiController]
public class AppointmentController(
    IAppointmentService _service,
    IMapper _mapper
) : CommonController
{
    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] AppointmentDto request
    )
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(_mapper.Map<Appointment>(request))));
    }
}
