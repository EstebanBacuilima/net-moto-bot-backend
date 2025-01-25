using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class EmployeeService(
    IEmployeeRepository _repository) : IEmployeeService
{
    public Task<List<Employee>> GetAllAsync(
        bool? active,
        string name = "",
        string idCard = ""
    )
    {
        return _repository.FindAllAsync(active, name, idCard);
    }

    public Task<Employee> CreateAsync(Employee employee)
    {
        return _repository.SaveAsync(employee);
    }

    public Task<Employee?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<Employee> UpdateAsync(Employee employee)
    {
        return _repository.UpdateAsync(employee);
    }

    public Task<Employee> UpdateActiveAsync(string code, bool active)
    {
        return _repository.UpdateActiveAsync(new()
        {
            Code = code,
            Active = active
        });
    }
}
