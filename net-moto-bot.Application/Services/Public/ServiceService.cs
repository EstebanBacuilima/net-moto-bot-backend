using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

using Service = net_moto_bot.Domain.Entities.Service;


namespace net_moto_bot.Application.Services.Public;

public class ServiceService(
    IServiceRepository _repository) : IServiceService
{
    public Task<Service> CreateAsync(Service service)
    {
        return _repository.SaveAsync(service);
    }

    public async Task<Service> UpdateAsync(Service service)
    {
        Service? finded = await _repository.FindByCodeAsync(service.Code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        finded.Name = service.Name;
        finded.Description = service.Description;
        finded.Icon = service.Icon;
        finded.Price = service.Price;
        finded.Active = service.Active;
        return await _repository.UpdateAsync(finded);
    }

    public Task<List<Service>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }

    public Task<Service?> GetByIdAsync(int id)
    {
        return _repository.FindByIdAsync(id);
    }

    public async Task<Service> ChangeStateAsync(string code , bool active)
    {
        Service? service = await _repository.FindByCodeAsync(code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        service.Active = active;

        await _repository.ChangeStateAsync(code, active);

        return service;
    }
}
