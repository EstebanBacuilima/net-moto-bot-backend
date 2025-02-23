using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class ProductSectionService(
    IProductSectionRepository _repository,
    ISectionRepository _sectionRepository) : IProductSectionService
{
    public async Task<List<ProductSection>> BulkCreateAsync(List<int> productIds, short sectionId)
    {
        List<ProductSection> productSections = [];

        foreach (int productId in productIds)
        {
            productSections.Add(new() { ProductId = productId, SectionId = sectionId });
        }

        await ValidateData(productSections);

        return await _repository.SaveRangeAsync(productSections);
    }

    public Task<List<ProductSection>> GetAllIncludingProductsAsync() 
    {
        return _repository.FindAllIncludingProductsAsync();
    }


    public static void SetData(List<ProductSection> productSections)
    {
        var filtered = productSections.DistinctBy(ps => new { ps.ProductId, ps.SectionId }).ToList();

        productSections.Clear();

        productSections.AddRange(filtered);

        short sectionId = productSections[0].SectionId;

        foreach (ProductSection productSection in productSections)
        {
            productSection.SectionId = sectionId;
        }
    }

    public async Task ValidateData(List<ProductSection> productSections)
    {
        if (productSections.Count == 0) throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.InvalidPoliticalDivisionType);

        SetData(productSections);

        short sectionId = productSections[0].SectionId;

        bool exists = _sectionRepository.ExistsById(sectionId);

        if (!exists) throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.InvalidMainStreet);

        await _repository.DeleteAllBySectionIdAsync(sectionId);
    }
}
