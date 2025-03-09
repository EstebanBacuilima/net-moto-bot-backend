using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IAppointmentRepository
{
    public Task<List<Appointment>> FindAllAsync(
        DateTime date,
        string name = "",
        string customerIdCard = "",
        string employeeIdCard = ""
    );
    public Task<Appointment> SaveAsync(Appointment appointment);
    public Task<Appointment?> FindByCodeAsync(string code);
    public Task<Appointment> UpdateAsync(Appointment appointment);
    public Task<Appointment> UpdateActiveAsync(Appointment appointment);
    public Task<Appointment> UpdateStateAsync(Appointment appointment);
}
