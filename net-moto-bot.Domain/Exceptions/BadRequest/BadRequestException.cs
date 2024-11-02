using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions;

namespace net_moto_bot.Domain.Exceptions.BadRequest;

public class BadRequestException : CustomException
{
    public BadRequestException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
