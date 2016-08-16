using JSpank.Chat.App.AutoMapper;
using JSpank.Chat.App.Interfaces.Services;
using JSpank.Chat.App.ValueObjects;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.App.Services
{
    public class UserAppService : IUserAppService
    {
        readonly IUserService _IUserService;

        public UserAppService(IUserService _IUserService)
        {
            this._IUserService = _IUserService;
        }        

        public ChatModelApp Add(string apiService, Guid dbid, string username, string username_add)
        {
            var result = this._IUserService.Add(apiService, dbid, username, username_add);
            return AutoMapperConfig.Get<ChatModel, ChatModelApp>(result);
        }

        public ChatModelApp Remove(string apiService, System.Guid dbid, string username, string username_remove)
        {
            var result = this._IUserService.Remove(apiService, dbid, username, username_remove);
            return AutoMapperConfig.Get<ChatModel, ChatModelApp>(result);
        }
    }
}
