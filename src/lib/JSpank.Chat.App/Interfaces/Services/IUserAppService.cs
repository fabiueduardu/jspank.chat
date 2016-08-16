using JSpank.Chat.App.ValueObjects;
using System;

namespace JSpank.Chat.App.Interfaces.Services
{
    public interface IUserAppService
    {
        ChatModelApp Add(string apiService, Guid dbid, string username, string username_add);

        ChatModelApp Remove(string apiService, Guid dbid, string username, string username_remove);
    }
}
