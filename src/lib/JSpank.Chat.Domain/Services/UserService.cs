using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.Domain.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _IUserRepository;

        public UserService(IUserRepository _IUserRepository)
        {
            this._IUserRepository = _IUserRepository;
        }


        public ChatModel Add(string apiService, Guid dbid, string username, string username_add)
        {
            return this._IUserRepository.Add(apiService, dbid, username, username_add);
        }

        public ChatModel Remove(string apiService, Guid dbid, string username, string username_remove)
        {
            return this._IUserRepository.Remove(apiService, dbid, username, username_remove);
        }
    }
}
