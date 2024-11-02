using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions.BadRequest;

public class RequiredFieldException : BadRequestException
{
    public RequiredFieldException(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
