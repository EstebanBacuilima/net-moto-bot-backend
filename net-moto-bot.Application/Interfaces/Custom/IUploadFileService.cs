using Microsoft.AspNetCore.Http;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Custom;

public interface IUploadFileService
{
    public Task<ProductImage> UploadImageFile(IFormFile file);   

    public Task<List<ProductImage>> UploadFiles(List<IFormFile> files);

    public Task<string> UploadImageFileAsync(IFormFile file);

    public Task<List<string>> UploadFilesAsync(List<IFormFile> files);

    public int DeleteAllFilesAsync(List<string> filesPath);
}
