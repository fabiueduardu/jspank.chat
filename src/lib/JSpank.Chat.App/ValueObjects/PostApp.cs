using System;

namespace JSpank.Chat.App.ValueObjects
{
    public class PostApp
    {
        public int PostId { get; set; }

        public string post { get; set; }

        public DateTime DateCreate { get; set; }

        public string UserName { get; set; }
    }
}
