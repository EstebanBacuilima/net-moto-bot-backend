using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions.BadRequest;

public class InvalidFieldException : BadRequestException
{
    public InvalidFieldException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
