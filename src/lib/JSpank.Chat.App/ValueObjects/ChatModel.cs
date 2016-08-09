using System;

namespace JSpank.Chat.App.ValueObjects
{
    public class ChatModelApp
    {
        public Guid DbId { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

    }
}
