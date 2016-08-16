using JSpank.Chat.App.ValueObjects;
using System;

namespace JSpank.Chat.App.Interfaces.Services
{
    public interface IPostAppService
    {
        ChatModelApp Post(string apiService, Guid dbid, string username, string post);

        GetChatModelApp Get(string apiService, Guid dbid, string username);
    }
}
