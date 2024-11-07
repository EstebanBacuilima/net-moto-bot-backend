using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

using Service = net_moto_bot.Domain.Entities.Service;


namespace net_moto_bot.Application.Services.Public;

public class ServiceService(IServiceRepository _repository) : IServiceService
{
    public Task<Service> CreateAsync(Service service)
    {
        return _repository.SaveAsync(service);
    }

    public async Task<Service> UpdateAsync(Service service)
    {
        Service? finded = await _repository.FindByIdAsync(service.Id)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        finded.Name = service.Name;
        finded.Description = service.Description;

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

    public async Task<Service> ChangeStateAsync(int id, bool active)
    {
        Service? service = await _repository.FindByIdAsync(id)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        service.Active = active;

        await _repository.ChangeStateAsync(id, active);

        return service;
    }
}
