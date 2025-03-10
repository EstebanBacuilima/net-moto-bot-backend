
using net_moto_bot.Domain.Models;

namespace net_moto_bot.Domain.Interfaces.Integration;

public interface IEmailRepository
{
    public Task<string> SendEmailAsync(EmailModel email);
}
