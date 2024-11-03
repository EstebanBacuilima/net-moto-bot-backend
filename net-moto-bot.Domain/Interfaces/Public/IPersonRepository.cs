using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IPersonRepository
{
    public Task<Person> SaveAsync(Person person);
    public Task<Person?> FindByCodeAsync(string code);
    public Task<Person> UpdateAsync(Person person);
    public Task<Person> UpdateActiveAsync(Person person);
    public Task<bool> ExistIdCardAsync(string idCard);
}
