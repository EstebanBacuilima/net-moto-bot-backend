using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions.Unauthorized;

public class BadCredentialException : UnauthorizedException
{
    public BadCredentialException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
