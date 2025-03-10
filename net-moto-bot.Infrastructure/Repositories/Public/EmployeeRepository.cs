﻿using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class EmployeeRepository(
    PostgreSQLContext _context
) : IEmployeeRepository
{
    public Task<List<Employee>> FindAllAsync(bool? active, string name = "", string idCard = "")
    {
        return _context.Employees
          .AsNoTracking()
          .Include(e => e.Person)
          .Where(e => (string.IsNullOrEmpty(name) || e.Person.FirstName.ToUpper().Contains(name.ToUpper())) &&
                      (string.IsNullOrEmpty(idCard) || e.Person.IdCard.ToUpper().Contains(idCard.ToUpper())) &&
                      (!active.HasValue || active.Value == e.Active))
          .ToListAsync();
    }

    public Task<Employee?> FindByCodeAsync(string code)
    {
        return _context.Employees
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Employee> SaveAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateActiveAsync(Employee employee)
    {
        var finded = await _context.Employees.FirstAsync(c => c.Code.Equals(employee.Code));
        finded.Active = employee.Active;
        await _context.SaveChangesAsync();
        return finded;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        var finded = await _context.Employees.FirstAsync(c => c.Code.Equals(employee.Code));
        finded.Active = employee.Active;
        await _context.SaveChangesAsync();
        return finded;
    }
}
