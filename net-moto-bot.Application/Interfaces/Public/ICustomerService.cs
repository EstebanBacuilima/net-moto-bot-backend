
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface ICustomerService
{
    public Task<List<Customer>> GetAllAsync(
        bool? active,
        string name = "",
        string idCard = ""
    );
    public Task<Customer> CreateAsync(Customer customer);
    public Task<Customer?> GetByCodeAsync(string code);
    public Task<Customer> UpdateAsync(Customer customer);
    public Task<Customer> UpdateActiveAsync(string code, bool active);
}
