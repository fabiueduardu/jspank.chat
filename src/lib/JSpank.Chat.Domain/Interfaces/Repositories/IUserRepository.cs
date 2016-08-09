using JSpank.Chat.Domain.Entities;

namespace JSpank.Chat.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
         User Get(string username);

    }
}
