using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace JSpank.Chat.Infra.Data.Repositories
{
    public class BaseRepository
    {
        protected T getJSON<T>(string apiService, string method = "POST", IDictionary<string, object> values = null)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    if (!method.Equals("POST"))
                    {
                        foreach (var param in values)
                            webClient.QueryString.Add(param.Key, param.Value.ToString());

                        string result = webClient.DownloadString(apiService);
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                    else
                    {

                        webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        var dataString = string.Join("&", values.Select(a => string.Concat(a.Key, "=", a.Value)).ToArray());

                        string result = webClient.UploadString(apiService, dataString);
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
            }
            catch
            {
                return default(T);
            }
        }

    }
}
