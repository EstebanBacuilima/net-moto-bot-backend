using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class AppointmentRepository(
    PostgreSQLContext _context
) : IAppointmentRepository
{
    public Task<List<Appointment>> FindAllAsync(
        DateTime date,
        string name = "",
        string customerIdCard = "",
        string employeeIdCard = ""
    )
    {
        return _context.Appointments
            .AsNoTracking()
            .Where(a =>
                   (string.IsNullOrEmpty(name) || a.Establishment.Name.ToUpper().Contains(name.ToUpper())) &&
                    (string.IsNullOrEmpty(employeeIdCard) || a.Employee.Person.IdCard.ToUpper().Contains(employeeIdCard.ToUpper())) &&
                     (string.IsNullOrEmpty(customerIdCard) || a.Customer.Person.IdCard.ToUpper().Contains(customerIdCard.ToUpper()))
            )
            .ToListAsync();
    }

    public Task<Appointment?> FindByCodeAsync(string code)
    {
        return _context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Code.Equals(code));
    }

    public async Task<Appointment> SaveAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment> UpdateActiveAsync(Appointment appointment)
    {
        var finded = await _context.Appointments.FirstAsync(c => c.Code.Equals(appointment.Code));
        finded.Observation = appointment.Observation;
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment> UpdateAsync(Appointment appointment)
    {
        var finded = await _context.Appointments.FirstAsync(c => c.Code.Equals(appointment.Code));
        finded.Active = appointment.Active;
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment> UpdateStateAsync(Appointment appointment)
    {
        var finded = await _context.Appointments.FirstAsync(c => c.Code.Equals(appointment.Code));
        finded.State = appointment.State;
        await _context.SaveChangesAsync();
        return appointment;
    }
}
