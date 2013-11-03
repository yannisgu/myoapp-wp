using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOApp.Library.Models
{
    public class OeventsLoader
    {
        string BASE_URL = "http://oevents.aws.af.cm/";

        public async Task<List<Event>> GetEvents()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + "events?source=solv");
            var response = await httpClient.SendAsync(request);

            var events = JsonConvert.DeserializeObject<Event[]>( await response.Content.ReadAsStringAsync());
            return events.ToList();
        }

    }
}
