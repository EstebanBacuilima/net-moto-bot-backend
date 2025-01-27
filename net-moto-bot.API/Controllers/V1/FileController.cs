using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Custom;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/file")]
[ApiController]
//[Authorize]
public class FileController(IUploadFileService _service) : CommonController
{
    [HttpPost, Route("bulk-create")]
    public async Task<IActionResult> BulkCreateAsync(
        [FromForm] List<IFormFile> files)
    {
        return Ok(ResponseHandler.Ok(await _service.UploadFilesAsync(files)));
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromForm] IFormFile file)
    {
        return Ok(ResponseHandler.Ok(await _service.UploadImageFileAsync(file)));
    }
}
