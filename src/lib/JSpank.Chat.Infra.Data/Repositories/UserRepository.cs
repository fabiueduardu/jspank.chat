using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace JSpank.Chat.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public ChatModel Add(string apiService, Guid dbid, string username, string username_add)
        {
            var values = new Dictionary<string, object>();
            values.Add("method", "friend");
            values.Add("dbid", dbid);
            values.Add("username", username);
            values.Add("username_add", username_add);

            var result = base.getJSON<ChatModel>(apiService, method: "POST", values: values);
            return result;
        }

        public ChatModel Remove(string apiService, Guid dbid, string username, string username_remove)
        {
            var values = new Dictionary<string, object>();
            values.Add("method", "friend");
            values.Add("dbid", dbid);
            values.Add("username", username);
            values.Add("username_remove", username_remove);

            var result = base.getJSON<ChatModel>(apiService, method: "POST", values: values);
            return result;
        }
    }
}
