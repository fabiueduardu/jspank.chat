using System;

namespace JSpank.Chat.Domain.Entities
{
    public class User
    {
        public string UserName { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreate { get; set; }

        public int PostId { get; set; }
    }
}
