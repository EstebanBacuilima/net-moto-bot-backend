
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class CustomerService(
    ICustomerRepository _repository) : ICustomerService
{
    public Task<List<Customer>> GetAllAsync(
        bool? active,
        string name = "",
        string idCard = ""
    )
    {
        return _repository.FindAllAsync(active, name, idCard);
    }

    public Task<Customer> CreateAsync(Customer customer)
    {
        return _repository.SaveAsync(customer);
    }

    public Task<Customer?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<Customer> UpdateAsync(Customer customer)
    {
        return _repository.UpdateAsync(customer);
    }

    public Task<Customer> UpdateActiveAsync(string code, bool active)
    {
        return _repository.UpdateActiveAsync(new()
        {
            Code = code,
            Active = active
        });
    }
}
