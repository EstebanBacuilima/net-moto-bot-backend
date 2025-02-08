using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class PersonRepository(
    PostgreSQLContext _context
) : IPersonRepository
{
    public Task<bool> ExistIdCardAsync(string idCard)
    {
        return _context.People
            .AsNoTracking()
            .AnyAsync(p => p.IdCard.Trim().Equals(idCard.Trim()));
    }

    public Task<Person?> FindByCodeAsync(string code)
    {
        return _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);
    }

    public Task<List<Person>> FindByIdCardOrNameOrLastNameAsync(string idCard = "", string name = "", string lastName = "")
    {
        return _context.People
         .AsNoTracking()
         .Where(p =>
                (string.IsNullOrEmpty(idCard) || p.IdCard.ToUpper().Contains(idCard.ToUpper())) &&
                 (string.IsNullOrEmpty(name) || p.FirstName.ToUpper().Contains(name.ToUpper())) &&
                  (string.IsNullOrEmpty(lastName) || p.LastName.ToUpper().Contains(lastName.ToUpper()))
         )
         .ToListAsync();
    }

    public async Task<Person> SaveAsync(Person person)
    {
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<Person> UpdateActiveAsync(Person person)
    {
        var finded = await _context.People.FirstAsync(b => b.Code.Equals(person.Code));
        finded.Active = person.Active;
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        var finded = await _context.People.FirstAsync(b => b.Code.Equals(person.Code));
        finded.FirstName = person.FirstName;
        finded.LastName = person.LastName;
        finded.Email = person.Email;
        await _context.SaveChangesAsync();
        return person;
    }
}
