using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class CategoryService(
    ICategoryRepository _repository
) : ICategoryService
{
}
