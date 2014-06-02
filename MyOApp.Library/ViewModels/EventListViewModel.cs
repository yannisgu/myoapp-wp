using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.DataLoader;
using MyOApp.Library.Helpers;
using MyOApp.Library.Models;

namespace MyOApp.Library.ViewModels
{
    [Magic]
    public class EventListViewModel : MvxViewModel
    {
        public event EventHandler ItemsLoaded;

        protected virtual void OnItemsLoaded()
        {
            EventHandler handler = ItemsLoaded;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private Task<ObservableCollection<EventItemViewModel>> LoadItemsCore()
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

            get { return overviewEdit; }
        }

        public async Task Init()
        {
            var itemsLoadedFired = false;
            ObservableCollection<EventItemViewModel> items;
            await LoadItems();
            if (Items.Any())
            {
                itemsLoadedFired = true;
                OnItemsLoaded();
                items = Items;
            }
            else
            {
                items = new ObservableCollection<EventItemViewModel>();
            }

            try
            {
                var last = Settings.LastModification;
                await (new OeventsLoader()).LoadEvents(last, items);
                if (items != Items)
                {
                    Items = items;
                }
                Settings.LastModification = Helper.GetTimestamp(DateTime.Now);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace + "\n" + ex.Message);
            }
            if (!itemsLoadedFired)
            {
                OnItemsLoaded();
            }
        }

        public ICommand ListItemClicked
        {
            get
            {
                return new MvxCommand<EventItemViewModel>((@event) =>
                {
                    this.SelectedItem = @event;
                    DisplayDetailCommand.Execute(null);
                });
            }
        }

        
    }
}
