using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions.Conflict;

public class UniqueKeyException : ConflictException
{
    public UniqueKeyException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
