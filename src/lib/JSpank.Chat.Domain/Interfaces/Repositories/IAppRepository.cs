
using JSpank.Chat.Domain.ValueObjects;
using System;
namespace JSpank.Chat.Domain.Interfaces.Repositories
{
    public interface IAppRepository
    {
        ChatModel New(string apiService, string username);
    }
}
