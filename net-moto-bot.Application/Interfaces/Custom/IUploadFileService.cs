using Microsoft.AspNetCore.Http;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Custom;


public interface IUploadFileService
{
    /// <summary>
    /// Upload image to the server.
    /// </summary>
    /// <param name="file">A <see cref="IFormFile"/> the contains data.</param>
    /// <param name="userCode">The user code.</param>
    /// <param name="microserviceName">The name microservice.</param>
    /// <returns>A URL indicating the route.</returns>
    public Task<ProductFile> UploadImageFile(IFormFile file);

    /// <summary>
    /// Uploads the files.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <param name="userCode">The user code.</param>
    /// <param name="microserviceName">The name microservice.</param>
    /// <returns>A url list of the image.</returns>
    public Task<List<ProductFile>> UploadFiles(List<IFormFile> files);

    /// <summary>
    /// Remove unnecessary files.
    /// </summary>
    /// <param name="filesPath">The files path.</param>
    /// <returns>Remove count.</returns>
    public int DeleteAllFilesAsync(List<string> filesPath);
}
