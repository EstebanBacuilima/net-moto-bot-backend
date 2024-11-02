using net_moto_bot.Domain.Enums.Custom;

namespace net_moto_bot.Domain.Exceptions.BadRequest;

public class EntityAlreadyExists : BadRequestException
{
    public EntityAlreadyExists(ExceptionEnum exceptionEnum) : base(exceptionEnum)
    {
    }
}
