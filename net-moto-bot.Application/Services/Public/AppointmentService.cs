using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class AppointmentService(
    IAppointmentRepository _repository
) : IAppointmentService
{
    public Task<Appointment> CreateAsync(Appointment appointment)
    {
        throw new NotImplementedException();
    }

    public Task<List<Appointment>> GetAllByDateAndIdCardAsync(DateTime date, string idCard)
    {
        return _repository.FindAllAsync(date,  customerIdCard: idCard);
    }
}
