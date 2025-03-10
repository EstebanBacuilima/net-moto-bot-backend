using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Domain.Models;
using net_moto_bot.Infrastructure.Connections;
using System.Data;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class AppointmentRepository(
    PostgreSQLContext _context,
    IDbConnection _dapper
) : IAppointmentRepository
{
    public List<DataChart> FindAllServiceDataChart()
    {
        string sql = $@"
            select 
	            s.name,
	            count(a.service_id) as quantity
            from services s 
            left join appointments a on a.service_id = s.id 
            group by name, service_id";

       return _dapper.Query<DataChart>(sql).ToList();
    }

    public List<DataChart> FindAllEstablishmentDataChart() 
    {
        string sql = $@"
            select 
	            e.name,
	            count(a.establishment_id) as quantity
            from establishments e 
            left join appointments a on a.establishment_id = e.id  
            group by name, establishment_id";

        return _dapper.Query<DataChart>(sql).ToList();
    }

    public List<DataChart> FindAllStateDataChart()
    {
        string sql = $@"
            select 
	            state,
	            case 
		            when state = 'P' then 'Pendiente'
		            when state = 'A' then 'Aprovado'
		            when  state = 'D' then 'Denegado'
		            else 'Desconocido'
	            end as name,
	            count(*) as quantity
            from appointments a 
            group by state";

        return _dapper.Query<DataChart>(sql).ToList();
    }

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
                    (string.IsNullOrEmpty(employeeIdCard) || a.Employee!.Person!.IdCard.ToUpper().Contains(employeeIdCard.ToUpper())) &&
                     (string.IsNullOrEmpty(customerIdCard) || a.Customer!.Person!.IdCard.ToUpper().Contains(customerIdCard.ToUpper()))
            )
            .Include(a => a.Customer)
                .ThenInclude(c => c.Person)
            .Include(a => a.Employee)
                .ThenInclude(e => e.Person)
            .Include(a => a.Service)
            .Include(a => a.Establishment)
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
        return finded;
    }
}
