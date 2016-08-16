using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.Domain.Interfaces.Services
{
    public interface IUserService
    {
        ChatModel Add(string apiService, Guid dbid, string username, string username_add);

        ChatModel Remove(string apiService, Guid dbid, string username, string username_remove);
    }
}
