using MyOApp.Library.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MyOApp.Library.ViewModels
{
    public class MapOverviewViewModel : PropertyChangedBase
    {
        public MapOverviewViewModel(string mapName)
        {
            MapName = mapName;
        }


        public string MapName
        {
            get;
            set;
        }

        private List<MapItemViewModel> maps;
        public List<MapItemViewModel> Maps
        {
            get
            { return maps; }
            set
            {
                maps = value;
                RaisePropertyChanged("Maps");
                RaisePropertyChanged("NoMaps");
            }
        }

        public  bool NoMaps
        {
            get
            {
                return Maps != null && Maps.Count == 0;
            }
        }

        public async Task LoadMaps()
        {
        
                var httpClient = new HttpClient();
                string baseUrl = "http://worldofo.com/m/findomaps.php?type=search&search={0}";
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(baseUrl, MapName));
                var response = await httpClient.SendAsync(request);

                var dataObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                var maps = JsonConvert.DeserializeObject<MapItemViewModel[]>(dataObject["data"].ToString());
                Maps = maps.ToList();
    }
    }
}
