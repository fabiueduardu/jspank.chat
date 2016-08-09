using JSpank.Chat.App.AutoMapper;
using JSpank.Chat.App.Interfaces.Services;
using JSpank.Chat.App.ValueObjects;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.App.Services
{
    public class AppAppService : IAppAppService
    {
        readonly IAppService _IAppService;

        public AppAppService(IAppService _IAppService)
        {
            this._IAppService = _IAppService;
        }

        public ChatModelApp New(string apiService, string username)
        {
            var result = this._IAppService.New(apiService, username);
            return AutoMapperConfig.Get<ChatModel, ChatModelApp>(result);
        }
    }
}
