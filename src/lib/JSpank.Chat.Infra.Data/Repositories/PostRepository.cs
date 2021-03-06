﻿using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace JSpank.Chat.Infra.Data.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public ChatModel Post(string apiService, Guid dbid, string username, string post)
        {
            var values = new Dictionary<string, object>();
            values.Add("method", "post");
            values.Add("dbid", dbid);
            values.Add("username", username);
            values.Add("post", post);

            var result = base.getJSON<ChatModel>(apiService, method: "POST", values: values);
            return result;
        }

        public GetChatModel Get(string apiService, Guid dbid, string username)
        {
            var values = new Dictionary<string, object>();
            values.Add("method", "get");
            values.Add("dbid", dbid);
            values.Add("username", username);

            var result = base.getJSON<GetChatModel>(apiService, method: "GET", values: values);
            return result;
        }
    }
}
