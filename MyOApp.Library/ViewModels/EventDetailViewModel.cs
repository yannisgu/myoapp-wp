using System;
using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.WebBrowser;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.DataLoader;
using MyOApp.Library.Models;
using System.Threading.Tasks;

namespace MyOApp.Library.ViewModels
{

    [Magic]
    public class EventDetailViewModel : MvxViewModel
    {
        private Event model;
        private readonly IMvxWebBrowserTask webBrowser;

        public EventDetailViewModel(IMvxWebBrowserTask webBrowser)
        {
            this.webBrowser = webBrowser;
        }

        public Event Model
        {
            get { return model; }
            set { model = value; }
        }

        public void LoadDataModel()
        {
            Name = model.Name;
        }


        public async void Init(Event @event)
        {
            Model = @event;
            LoadDataModel();
        }

        public string Name { get; set; }

        public MapsListViewModel MapViewModel { get; set; }

        public ICommand OpenMapsCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {

                    ShowViewModel<MapsListViewModel>(new MapsListParameters()
                    {
                        MapName = Model.Map

                    });
                });
            }
        }


        public ICommand OpenInformations
        {
            get
            {
                return new MvxCommand(() =>
                {
                    if (Model.Url != null && Uri.IsWellFormedUriString(Model.Url, UriKind.Absolute))
                    {
                        webBrowser.ShowWebPage(Model.Url);
                    }
                });
            }
        }

        public ICommand OpenTransports
        {
            get
            {
                return new MvxCommand(() =>
                {
                    var to = "";
                    if (!string.IsNullOrEmpty(model.EventCenter))
                    {
                        to = "to=" + model.EventCenter;
                    }
                    else if (model.EventCenterLatitude > 0 && model.EventCenterLongitude > 0)
                    {
                        to = "toll=" + model.EventCenterLongitude + ',' + model.EventCenterLatitude;

                    }
                    var date = model.UnixTimestamp / 1000;
                    var timetableUrl = "sbbmobileb2c://timetable?" + to + "&time=" + date +
                                       "&accessid=dm89518e7a4e0bcf670";

                    try
                    {
                        webBrowser.ShowWebPage(timetableUrl);
                    }
                    catch (Exception)
                    {
                        // App not installed
                    }
                });
            }
        }
}
}
