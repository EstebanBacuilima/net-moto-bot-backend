using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class CustomerRepository(
    PostgreSQLContext _context
) : ICustomerRepository
{
    public Task<bool> ExistsByIdCard(string idCard)
    {
        return _context.Customers
            .AsNoTracking()
            .AnyAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public Task<List<Customer>> FindAllAsync(bool? active, string name = "", string idCard = "")
    {
        return _context.Customers
            .AsNoTracking()
            .Include(e => e.Person)
            .Where(c => (string.IsNullOrEmpty(name) || c.Person.FirstName.ToUpper().Contains(name.ToUpper())) &&
                        (string.IsNullOrEmpty(idCard) || c.Person.IdCard.ToUpper().Contains(idCard.ToUpper())) &&
                        (!active.HasValue || active.Value == c.Active))
            .ToListAsync();
    }

    public Task<Customer?> FindByCodeAsync(string code)
    {
        return _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Code == code);
    }

    public Task<Customer?> FindByIdCardAsync(string idCard)
    {
        return _context.Customers
                  .AsNoTracking()
                  .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public async Task<Customer> SaveAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateActiveAsync(Customer customer)
    {
        var finded = await _context.Customers.FirstAsync(c => c.Code.Equals(customer.Code));
        finded.Active = customer.Active;
        await _context.SaveChangesAsync();
        return finded;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        var finded = await _context.Customers.FirstAsync(c => c.Code.Equals(customer.Code));
        finded.Active = customer.Active;
        await _context.SaveChangesAsync();
        return finded;
    }

    public Customer FindById(int id) 
    {
        return _context.Customers.AsNoTracking()
            .Include(c => c.Person)
            .First(c => c.Id == id);
    }
}
