using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace JSpank.Chat.Infra.Data.Repositories
{
    public class AppRepository : BaseRepository, IAppRepository
    {
        public ChatModel New(string apiService, string username)
        {
            var values = new Dictionary<string, object>();
            values.Add("username", username);

            var result = base.getJSON<ChatModel>(string.Concat(apiService, _new), method: "GET", values: values);
            return result;
        }
    }
}
