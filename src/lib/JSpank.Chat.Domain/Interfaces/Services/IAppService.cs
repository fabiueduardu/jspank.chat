
using JSpank.Chat.Domain.ValueObjects;
using System;
namespace JSpank.Chat.Domain.Interfaces.Services
{
    public interface IAppService
    {
        ChatModel New(string apiService, string username);
    }
}
