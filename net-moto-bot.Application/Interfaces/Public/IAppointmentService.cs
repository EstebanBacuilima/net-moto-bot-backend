using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IAppointmentService
{
    public Task<Appointment> CreateAsync(Appointment appointment);
    public Task<List<Appointment>> GetAllByDateAndIdCardAsync(DateTime date, string idCard);
}
