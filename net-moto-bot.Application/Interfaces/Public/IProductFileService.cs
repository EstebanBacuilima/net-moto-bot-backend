
using Microsoft.AspNetCore.Http;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IProductFileService
{
    public Task<List<ProductImage>> BulkCreateAsync(List<IFormFile> uploadedFiles, int productId);

    public Task<ProductImage> CreateAsync(IFormFile file, int productId);

    public Task<List<ProductImage>> GetAllByProductIdAsync(int productId);

    public Task<List<ProductImage>> GetAllByProductCodeAsync(string productCode);

    public void ChangeState(string code, bool state);

    public void DeleteByCode(string code);
}
