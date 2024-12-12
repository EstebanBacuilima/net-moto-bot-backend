
using Microsoft.AspNetCore.Http;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IProductFileService
{
    public Task<List<ProductFile>> BulkCreateAsync(List<IFormFile> uploadedFiles, int productId);

    public Task<ProductFile> CreateAsync(IFormFile file, int productId);

    public Task<List<ProductFile>> GetAllByProductIdAsync(int productId);

    public Task<List<ProductFile>> GetAllByProductCodeAsync(string productCode);

    public void ChangeState(int id, bool state);
}
