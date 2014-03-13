using System;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MyOApp.Library.ViewModels
{
    [Magic]
    public class MapOverviewViewModel : MvxViewModel
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

        private List<MapItemModel> maps;
        public List<MapItemModel> Maps
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
            try
            {
                var httpClient = new HttpClient();
                string baseUrl = "http://worldofo.com/m/findomaps.php?type=search&search={0}";
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(baseUrl, MapName));
                var response = await httpClient.SendAsync(request);

                var dataObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                Maps= JsonConvert.DeserializeObject<MapItemModel[]>(dataObject["data"].ToString()).ToList();
            }
            catch (Exception)
            {
                if (Maps == null)
                {
                    Maps = new List<MapItemModel>();
                }
            }

                
    }
    }
}
