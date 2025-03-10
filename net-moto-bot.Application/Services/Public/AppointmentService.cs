using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Integration;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Domain.Models;

namespace net_moto_bot.Application.Services.Public;

public class AppointmentService(
    IAppointmentRepository _repository,
    ICustomerRepository _customerRepository,
    IEmailRepository _emailRepository
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
        appointment.Date = appointment.Date.ToLocalTime();
        // Create appointment
        Appointment appointmentResponse = await _repository.SaveAsync(appointment);

        _ = BuildEmailAsync(appointmentResponse);

        return appointmentResponse;
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

    public async Task<Appointment> UpdateStateAsync(Appointment appointment)
    {
        Appointment appointmentResponse = await _repository.UpdateStateAsync(appointment);

        _ = BuildEmailAsync(appointmentResponse);

        return appointmentResponse;
    }

    public async Task SendMailAsync(EmailModel email)
    {
        await _emailRepository.SendEmailAsync(email);
    }


    public async Task BuildEmailAsync(Appointment appointment)
    {
        Customer customer = _customerRepository.FindById(appointment.CustomerId);

        string fullName = customer.Person.FirstName + " " + customer.Person.LastName;

        string idCard = customer.Person.IdCard;

        EmailModel emailModel = new()
        {
            To = customer.Person.Email!,
            Subject = "ESTADO DE CITA",
            Message = $@"HOLA {fullName} - {idCard}. {GetStatus(appointment.State.ToString())}"
        };

        await SendMailAsync(emailModel);
    }

    public static string GetStatus(string status)
    {
        return status switch
        {
            "A" => "Hemos procedido a APROVAR su cita.",
            "D" => "Hemos DENEGADO su cita por algunas inconcistecias",
            "P" => "Su estado de cita se encuentra PENDIENTE.",
            _ => "DENEGADO",
        };
    }
}
