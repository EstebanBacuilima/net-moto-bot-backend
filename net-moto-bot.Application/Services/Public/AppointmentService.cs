using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class AppointmentService(
    IAppointmentRepository _repository,
    ICustomerRepository _customerRepository
) : IAppointmentService
{
    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        // Create or get customer
        Customer? customerSaved;
        if (await _customerRepository.ExistsByIdCard(appointment.Customer?.Person?.IdCard ?? string.Empty))
            customerSaved = await _customerRepository.FindByIdCardAsync(appointment.Customer?.Person?.IdCard ?? string.Empty);
        else
            customerSaved = await _customerRepository.SaveAsync(appointment.Customer ?? new());

        if (customerSaved == null) throw new BadRequestException(ExceptionEnum.InvalidIdCard);
        appointment.CustomerId = customerSaved.Id;
        // Create appointment
        return await _repository.SaveAsync(appointment);
    }

    public Task<List<Appointment>> GetAllByDateAndIdCardAsync(
        DateTime date,
        string name = "",
        string customerIdCard = "",
        string employeeIdCard = ""
    )
    {
        return _repository.FindAllAsync(date, customerIdCard: customerIdCard, employeeIdCard: employeeIdCard, name: name);
    }
}
