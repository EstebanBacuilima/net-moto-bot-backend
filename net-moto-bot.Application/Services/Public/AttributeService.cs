using net_moto_bot.Application.Interfaces.Public;
using Attribute = net_moto_bot.Domain.Entities.Attribute;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;

namespace net_moto_bot.Application.Services.Public;

public class AttributeService(
    IAttributeRepository _repository) : IAttributeService
{
    public Task<Attribute> CreateAsync(Attribute attribute)
    {
        Validate(attribute);

        return _repository.SaveAsync(attribute);
    }

    public async Task<Attribute> UpdateAsync(Attribute attribute)
    {

        Attribute? finded = await _repository.FindByCodeAsync(attribute.Code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        finded.Name = attribute.Name;
        finded.Description = attribute.Description;

        Validate(finded);

        return await _repository.UpdateAsync(finded);
    }

    public Task<List<Attribute>> GetAllAsync(string value)
    {
        return _repository.FindAllAsync(value);
    }

    public Task<Attribute?> GetByIdAsync(int id)
    {
        return _repository.FindByIdAsync(id);
    }

    public async Task<Attribute> ChangeStateAsync(string code, bool active)
    {
        Attribute? attribute = await _repository.FindByCodeAsync(code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        attribute.Active = active;

        await _repository.ChangeStateAsync(code, active);

        return attribute;
    }

    private void Validate(Attribute attribute)
    {
        if (string.IsNullOrWhiteSpace(attribute.Name)) throw new BadRequestException(ExceptionEnum.InvalidName);

        Attribute? finded = _repository.FindByName(attribute.Name);

        if (finded != null)
        {
            if (!(attribute.Id == finded.Id)) throw new BadRequestException(ExceptionEnum.NameIsAlreadyExists);
        }
    }
}
