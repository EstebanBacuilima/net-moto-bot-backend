using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions;

namespace net_moto_bot.Domain.Exceptions.Unauthorized;

public class UnauthorizedException : CustomException
{
    public UnauthorizedException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
