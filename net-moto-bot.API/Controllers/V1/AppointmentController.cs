using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Dtos.Custom;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

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

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery(Name = "date")] DateTime date,
        [FromQuery(Name = "name")] string name = "",
        [FromQuery(Name = "customer-id-card")] string customerIdCard = "",
        [FromQuery(Name = "employee-id-card")] string employeeIdCard = ""
    )
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllByDateAndIdCardAsync(date, customerIdCard, employeeIdCard, name)));
    }

    [HttpPut, Route("update-state/{code}")]
    public async Task<IActionResult> UpdateStateAsync(
        [FromBody] AppointmentDto request,
        string code
    )
    {
        request.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateStateAsync(_mapper.Map<Appointment>(request))));
    }
}
