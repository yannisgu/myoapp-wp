using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.Helpers;
using MyOApp.Library.Models;

namespace MyOApp.Library.ViewModels
{
    [Magic]
    public class EventListViewModel : MvxViewModel
    {
        Task<ObservableCollection<EventItemViewModel>> LoadItemsCore()
        {
            return Task.Run(async () =>
            {
                var events = await Platform.DataAccess.GetEvents();
                var models = from i in events
                             orderby i.Date
                             select new EventItemViewModel(i);
                return new ObservableCollection<EventItemViewModel>(models);
            });
        }

        public async Task LoadItems()
        {
            IsLoading = true;
            try
            {
                Items = await LoadItemsCore();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public bool IsLoading { get; private set; }

        public ObservableCollection<EventItemViewModel> Items { get; set; }


        public EventItemViewModel SelectedItem { get; set; }
        /*
        async void UpdateDetails()
        {
            if (selectedItem == null)
                DetailItem = null;
            else if (DetailItem == null || DetailItem.Model.Id != selectedItem.Id)
            {
                IsLoading = true;
                try
                {
                    var item = await LoadDetails(selectedItem.Id);
                    DetailItem = new EventDetailViewModel(item);
                }
                catch 
                {
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }*/

       /* Task<Event> LoadDetails(int id)
        {
            return Platform.DataAccess.GetEvent(id);
        }*/

        //public EventDetailViewModel DetailItem { get;  set; }

        public ICommand DisplayDetailCommand
        {
            get { return new MvxCommand(() => ShowViewModel<EventDetailViewModel>(SelectedItem.Event)); }
        }

       
        private bool overviewEdit;

        public bool OverviewEdit
        {
            set
            {
                overviewEdit = value;
                lock (Items)
                {
                    if (Items != null)
                    {
                        foreach (var item in Items)
                        {
                            item.EditMode = value;
                        }
                    }
                }

            }

            get
            {
                return overviewEdit;
            }
        }

        public async Task Init()
        {

            await LoadItems();

            //if (NetworkInterface.NetworkInterfaceType != Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None)
            //{
            try
            {
                var last =  Settings.LastModification;
                //var task = (new OeventsLoader()).LoadEvents(last != null ? (long)last : 0, RootViewModel.Items);
                await (new OeventsLoader()).LoadEvents(last, Items);
                Settings.LastModification = Helper.GetTimestamp(DateTime.Now);
            }
            catch (Exception ex)
            {
                //Console.Out.Write(ex.Message);
            }
            //}


        }
    }
}
