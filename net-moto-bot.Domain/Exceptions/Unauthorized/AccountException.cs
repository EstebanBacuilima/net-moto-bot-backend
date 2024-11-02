using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions.Unauthorized;

public class AccountException : UnauthorizedException
{
    public AccountException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
