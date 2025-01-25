
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IEmployeeService
{
    public Task<List<Employee>> GetAllAsync(
        bool? active,
        string name = "",
        string idCard = ""
    );
    public Task<Employee> CreateAsync(Employee employee);
    public Task<Employee?> GetByCodeAsync(string code);
    public Task<Employee> UpdateAsync(Employee employee);
    public Task<Employee> UpdateActiveAsync(string code, bool active);
}
