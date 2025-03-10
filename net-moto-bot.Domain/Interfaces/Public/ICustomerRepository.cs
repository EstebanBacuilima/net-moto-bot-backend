using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface ICustomerRepository
{
    public Task<List<Customer>> FindAllAsync(
        bool? active,
        string name = "",
        string idCard = ""
    );
    public Task<bool> ExistsByIdCard(string idCard);
    public Task<Customer?> FindByIdCardAsync(string idCard);
    public Task<Customer> SaveAsync(Customer customer);
    public Task<Customer?> FindByCodeAsync(string code);
    public Task<Customer> UpdateAsync(Customer customer);
    public Task<Customer> UpdateActiveAsync(Customer customer);
    public Customer FindById(int id);
}
