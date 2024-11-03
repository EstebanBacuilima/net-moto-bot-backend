using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IEstablishmentRepository
{
    public Task<List<Establishment>> FindAllAsync(
      string name = "",
      string description = ""
    );
    public Task<Establishment> SaveAsync(Establishment establishment);
    public Task<Establishment?> FindByCodeAsync(string code);
    public Task<Establishment> UpdateAsync(Establishment establishment);
    public Task<Establishment> UpdateActiveAsync(Establishment establishment);
}
