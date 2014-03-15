using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyOApp.Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyOApp.Library.DataLoader
{
    public class OmapsLoader
    {
        public async Task<IEnumerable<Map>> GetMaps(string mapName)
        {
            var maps = new List<Map>();
            try
            {
                var httpClient = new HttpClient();
                string baseUrl = "http://worldofo.com/m/findomaps.php?type=search&search={0}";
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(baseUrl, mapName));
                var response = await httpClient.SendAsync(request);

                var dataObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                maps = JsonConvert.DeserializeObject<Map[]>(dataObject["data"].ToString()).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }

            return maps;
        }
    }
}
