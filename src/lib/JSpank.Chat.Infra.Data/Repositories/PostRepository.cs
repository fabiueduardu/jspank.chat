using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace JSpank.Chat.Infra.Data.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public ChatModel Post(string apiService, Guid dbid, string username, string post, string[] username_add, string[] username_remove)
        {
            var values = new Dictionary<string, object>();
            values.Add("dbid", dbid);
            values.Add("username", username);
            values.Add("post", post);
            values.Add("username_add", string.Join(",", username_add ?? new string[] { }));
            values.Add("username_remove", string.Join(",", username_remove ?? new string[] { }));

            var result = base.getJSON<ChatModel>(string.Concat(apiService, _post), method: "POST", values: values);
            return result;
        }

        public GetChatModel Get(string apiService, Guid dbid, string username)
        {
            var values = new Dictionary<string, object>();
            values.Add("dbid", dbid);
            values.Add("username", username);

            var result = base.getJSON<GetChatModel>(string.Concat(apiService, _get), method: "GET", values: values);
            return result;
        }
    }
}
