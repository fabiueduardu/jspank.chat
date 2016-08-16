using JSpank.Chat.App.AutoMapper;
using JSpank.Chat.App.Interfaces.Services;
using JSpank.Chat.App.ValueObjects;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.App.Services
{
    public class PostAppService : IPostAppService
    {
        readonly IPostService _IPostService;

        public PostAppService(IPostService _IPostService)
        {
            this._IPostService = _IPostService;
        }

        public ChatModelApp Post(string apiService, Guid dbid, string username, string post)
        {
            var result = this._IPostService.Post(apiService, dbid, username, post);
            return AutoMapperConfig.Get<ChatModel, ChatModelApp>(result);
        }

        public GetChatModelApp Get(string apiService, Guid dbid, string username)
        {
            var result = this._IPostService.Get(apiService, dbid, username);
            return AutoMapperConfig.Get<GetChatModel, GetChatModelApp>(result);
        }
    }
}
