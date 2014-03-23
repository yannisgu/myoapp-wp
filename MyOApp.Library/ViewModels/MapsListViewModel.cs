using System;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.WebBrowser;
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
        private readonly IMvxWebBrowserTask webBrowser;

        public MapsListViewModel(IMvxWebBrowserTask webBrowser)
        {
            this.webBrowser = webBrowser;
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
                return Maps != null && !Maps.Any();
            }
        }

        public async Task Init(MapsListParameters parameters)
        {
            MapName = parameters.MapName;
            Maps = await (new OmapsLoader()).GetMaps(MapName);

        }


        public ICommand OpenMap
        {
            get { return new MvxCommand<Map>((map) => webBrowser.ShowWebPage(map.ImageUrl)); }
        }

    }

    public class MapsListParameters
    {
        public string MapName { get; set; }
    }
}
