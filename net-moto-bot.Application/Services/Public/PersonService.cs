using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class PersonService(
    IPersonRepository _repository
) : IPersonService
{
    public Task<Person> CreateAsync(Person person)
    {
       return _repository.SaveAsync(person);
    }

    public Task<Person?> GetByCodeAsync(string code)
    {
       return _repository.FindByCodeAsync(code);
    }

    public Task<List<Person>> GetByIdCardOrNameOrLastNameAsync(
        string idCard = "", 
        string name = "", 
        string lastName = ""
    )
    {
        return _repository.FindByIdCardOrNameOrLastNameAsync(idCard, name, lastName);
    }

    public Task<Person> UpdateActiveAsync(Person person)
    {
        return _repository.UpdateActiveAsync(person);
    }

    public Task<Person> UpdateAsync(Person person)
    {
        return _repository.UpdateAsync(person);
    }
}
