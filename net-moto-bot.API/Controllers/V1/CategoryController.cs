using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Dtos.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/category")]
[ApiController]
//[Authorize]
public class CategoryController(
    ICategoryService _service,
    IMapper _mapper
) : CommonController
{
    [HttpPatch, Route("modify/change-state/{code}")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromBody] bool active, string code)
    {
        return Ok(ResponseHandler.Ok(await _service.UpdateActveAsync(code, active)));
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CategoryDTO category)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(_mapper.Map<Category>(category))));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync()));
    }

    [HttpGet, Route("find-by")]
    public async Task<IActionResult> GetByCode([FromQuery] string code)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByCodeAsync(code)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] CategoryDTO category, string code)
    {
        category.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(_mapper.Map<Category>(category))));
    }
}
