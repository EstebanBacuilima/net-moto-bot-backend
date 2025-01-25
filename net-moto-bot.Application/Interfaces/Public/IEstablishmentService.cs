using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IEstablishmentService
{
    public Task<List<Establishment>> GetAllAsync(
        bool? active,
        string name = "",
        string description = ""
    );
    public Task<Establishment> CreateAsync(Establishment establishment);
    public Task<Establishment?> GetByCodeAsync(string code);
    public Task<Establishment> UpdateAsync(Establishment establishment);
    public Task<Establishment> UpdateActiveAsync(string code, bool active);
}
