using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Dtos.Public.Request;

public class AppointmentDto
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int EstablishmentId { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public string Code { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public char State { get; set; } = 'P';

    public string? Observation { get; set; }

    public bool Active { get; set; }

    public CustomerDto Customer { get; set; } = new CustomerDto();
}
