using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IEmployeeRepository
{
    public Task<List<Employee>> FindAllAsync(
        bool? active,
        string name = "",
        string idCard = ""
    );
    public Task<Employee> SaveAsync(Employee employee);
    public Task<Employee?> FindByCodeAsync(string code);
    public Task<Employee> UpdateAsync(Employee employee);
    public Task<Employee> UpdateActiveAsync(Employee employee);
}
