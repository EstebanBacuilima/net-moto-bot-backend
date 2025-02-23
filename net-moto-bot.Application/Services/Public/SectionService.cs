
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class SectionService (
    ISectionRepository _repository) : ISectionService
{
    public Task<Section> CreateAsync(Section section)
    {
        Validate(section);

        return _repository.SaveAsync(section);
    }

    public async Task<Section> UpdateAsync(Section section)
    {

        Section? finded = await _repository.FindByCodeAsync(section.Code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        finded.Name = section.Name;
        finded.Description = section.Description;
        finded.EndDate = section.EndDate;

        Validate(finded);

        return await _repository.UpdateAsync(finded);
    }

    public Task<List<Section>> GetAllAsync(string value)
    {
        return _repository.FindAllAsync(value);
    }

    public Task<Section?> GetByIdAsync(int id)
    {
        return _repository.FindByIdAsync(id);
    }

    public async Task<Section> ChangeStateAsync(string code, bool active)
    {
        Section? attribute = await _repository.FindByCodeAsync(code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        attribute.Active = active;

        await _repository.ChangeStateAsync(code, active);

        return attribute;
    }

    public Task<List<Section>> GetAllIncludingProductsAsync()
    {
        return _repository.FindAllIncludingProductsAsync();
    }

    private static void SetData(Section section)
    {
        if (section.EndDate.HasValue) section.EndDate = section.EndDate.Value.ToUniversalTime();
    }

    private void Validate(Section section)
    {
        SetData(section);

        if (string.IsNullOrWhiteSpace(section.Name)) throw new BadRequestException(ExceptionEnum.InvalidName);

        Section? finded = _repository.FindByName(section.Name);

        if (finded != null)
        {
            if (!(section.Id == finded.Id)) throw new BadRequestException(ExceptionEnum.NameIsAlreadyExists);
        }
    }
}
