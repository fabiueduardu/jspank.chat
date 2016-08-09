using System;

namespace JSpank.Chat.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }

        public string post { get; set; }

        public DateTime DateCreate { get; set; }

        public string UserName { get; set; }
    }
}
