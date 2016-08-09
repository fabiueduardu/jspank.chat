﻿using JSpank.Chat.Domain.ValueObjects;
using System;

namespace JSpank.Chat.Domain.Interfaces.Services
{
    public interface IPostService
    {
        ChatModel Post(string apiService, Guid dbid, string username, string post, string[] username_add, string[] username_remove);

        GetChatModel Get(string apiService, Guid dbid, string username);
    }
}