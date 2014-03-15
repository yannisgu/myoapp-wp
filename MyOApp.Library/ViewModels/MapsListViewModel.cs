using System;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.DataLoader;
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
    public class MapsListViewModel : MvxViewModel
    {
        public MapsListViewModel()
        {
            
        }

        public MapsListViewModel(string mapName)
        {
            MapName = mapName;
        }


        public string MapName
        {
            get;
            set;
        }

        private IEnumerable<Map> maps;
        public IEnumerable<Map> Maps
        {
            get
            { return maps; }
            set
            {
                maps = value;
                RaisePropertyChanged(() => Maps);
                RaisePropertyChanged(() => NoMaps);
            }
        }

        public  bool NoMaps
        {
            get
            {
                return Maps != null && Maps.Count() == 0;
            }
        }

        public async Task Init(MapsListParameters parameters)
        {
            MapName = parameters.MapName;
            Maps = await (new OmapsLoader()).GetMaps(MapName);

        }

    }

    public class MapsListParameters
    {
        public IEnumerable<Map> Maps { get; set; }
        public string MapName { get; set; }
    }
}
