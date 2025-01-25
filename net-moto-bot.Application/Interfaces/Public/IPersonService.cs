using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IPersonService
{
    public Task<Person> CreateAsync(Person person);
    public Task<Person?> GetByCodeAsync(string code);
    public Task<List<Person>> GetByIdCardOrNameOrLastNameAsync(
        string idCard = "",
        string name = "",
        string lastName = ""
    );
    public Task<Person> UpdateAsync(Person person);
    public Task<Person> UpdateActiveAsync(Person person);
}
