using JSpank.Chat.Domain.Interfaces.Repositories;
using JSpank.Chat.Domain.ValueObjects;
using System.Collections.Generic;

namespace JSpank.Chat.Infra.Data.Repositories
{
    public class AppRepository : BaseRepository, IAppRepository
    {
        public ChatModel New(string apiService, string username)
        {
            var values = new Dictionary<string, object>();
            values.Add("method", "new");
            values.Add("username", username);

            var result = base.getJSON<ChatModel>(apiService, method: "GET", values: values);
            return result;
        }
    }
}
