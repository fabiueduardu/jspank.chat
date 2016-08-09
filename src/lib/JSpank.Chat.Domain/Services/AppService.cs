using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.Interfaces.Services;
using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.Domain.Services
{
    public class AppService : IAppService
    {
        readonly IAppRepository _IAppRepository;

        public AppService(IAppRepository _IAppRepository)
        {
            this._IAppRepository = _IAppRepository;
        }

        public ChatModel New(string apiService, string username)
        {
            return this._IAppRepository.New(apiService, username);
        }
    }
}
