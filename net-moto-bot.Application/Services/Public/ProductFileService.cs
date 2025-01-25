
using Microsoft.AspNetCore.Http;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class ProductFileService(
    IProductFileRepository _repository,
    IUploadFileService _uploadFileService) : IProductFileService
{
    public async Task<List<ProductFile>> BulkCreateAsync(List<IFormFile> uploadedFiles, int productId)
    {
        List<ProductFile> files = await _uploadFileService.UploadFiles(uploadedFiles);

        foreach (ProductFile productFile in files)
        {
            productFile.ProductId = productId;

        }
        return await _repository.SaveRangeAsync(files);
    }

    public async Task<ProductFile> CreateAsync(IFormFile file, int productId)
    {
        ProductFile fileCreated = await _uploadFileService.UploadImageFile(file);

        fileCreated.ProductId = productId;
        return await _repository.SaveAsync(fileCreated);
    }

    public Task<List<ProductFile>> GetAllByProductIdAsync(int productId)
    {
        return _repository.FindAllByProductIdAsync(productId);
    }

    public Task<List<ProductFile>> GetAllByProductCodeAsync(string productCode)
    {
        return _repository.FindAllByProductCodeAsync(productCode);
    }

    public void ChangeState(string code, bool state)
    {
        _repository.ChangeState(code, state);
    }

    public void DeleteByCode(string code) 
    {
        _repository.DeleteByCode(code);
    }
}
