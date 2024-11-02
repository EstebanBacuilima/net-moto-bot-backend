using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions;

namespace net_moto_bot.Domain.Exceptions.Forbidden;

public class ForbiddenException : CustomException
{
    public ForbiddenException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
