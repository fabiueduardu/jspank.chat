using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.Domain.Services
{
    public class PostService : IPostService
    {
        readonly IPostRepository _IPostRepository;

        public PostService(IPostRepository _IPostRepository)
        {
            this._IPostRepository = _IPostRepository;
        }

        public ChatModel Post(string apiService, Guid dbid, string username, string post, string[] username_add, string[] username_remove)
        {
            return this._IPostRepository.Post(apiService, dbid, username, post, username_add, username_remove);
        }

        public GetChatModel Get(string apiService, Guid dbid, string username)
        {
            return this._IPostRepository.Get(apiService, dbid, username);
        }
    }
}
