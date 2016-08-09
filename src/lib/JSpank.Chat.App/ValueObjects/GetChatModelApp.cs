using System.Collections.Generic;

namespace JSpank.Chat.App.ValueObjects
{
    public class GetChatModelApp : ChatModelApp
    {
        public IEnumerable<PostApp> Posts { get; set; }
    }
}
