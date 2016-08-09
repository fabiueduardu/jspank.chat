using JSpank.Chat.Domain.Entities;
using System.Collections.Generic;

namespace JSpank.Chat.Domain.ValueObjects
{
    public class GetChatModel : ChatModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
