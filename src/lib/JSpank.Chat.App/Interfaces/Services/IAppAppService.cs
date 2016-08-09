using JSpank.Chat.App.ValueObjects;
using System;

namespace JSpank.Chat.App.Interfaces.Services
{
    public interface IAppAppService
    {
        ChatModelApp New(string apiService, string username);
    }
}
