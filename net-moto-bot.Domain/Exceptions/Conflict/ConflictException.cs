using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions;

namespace net_moto_bot.Domain.Exceptions.Conflict;

public class ConflictException : CustomException
{
    public ConflictException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
