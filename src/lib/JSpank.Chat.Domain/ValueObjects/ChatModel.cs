using JSpank.Chat.Domain.Entities;
using System;
using System.Collections.Generic;

namespace JSpank.Chat.Domain.ValueObjects
{
    public class ChatModel
    {
        public Guid DbId { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

    }
}
