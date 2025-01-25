using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class EstablishmentService(
    IEstablishmentRepository _repository
) : IEstablishmentService
{
    public Task<List<Establishment>> GetAllAsync(string name = "", string description = "")
    {
        return _repository.FindAllAsync(name, description);
    }

    public Task<Establishment> SaveAsync(Establishment establishment)
    {
       return _repository.SaveAsync(establishment);
    }

    public Task<Establishment?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<Establishment> UpdateActiveAsync(Establishment establishment)
    {
        return _repository.UpdateActiveAsync(establishment);
    }

    public Task<Establishment> UpdateAsync(Establishment establishment)
    {
        return _repository.UpdateAsync(establishment);
    }
}
