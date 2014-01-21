using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyOApp.Library.Models;

namespace MyOApp.Library.ViewModels
{
    public class RootViewModel : PropertyChangedBase
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
        public ObservableCollection<EventItemViewModel> Items { get;  set; }

        EventItemViewModel selectedItem;
        public EventItemViewModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                UpdateDetails();
            }
        }


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
        }

        Task<Event> LoadDetails(int id)
        {
            return Platform.DataAccess.GetEvent(id);
        }

        public EventDetailViewModel DetailItem { get;  set; }

        public void EditEvent()
        {
            if (DetailItem == null)
                throw new InvalidOperationException();

            DetailItem.IsReadOnly = false;
        }

        public async Task SaveEvent()
        {
            var selectedItem = SelectedItem;
            var detailItem = DetailItem;

            var isNew = detailItem.IsNew;
            await detailItem.Save();

            if (isNew)
                Items.Add(SelectedItem = new EventItemViewModel(detailItem.Model));
            else if (selectedItem != null)
                selectedItem.LoadDataModel(detailItem.Model);
        }

        public async Task DeleteEvent()
        {
            var detail = DetailItem;
            if (detail == null)
                throw new InvalidOperationException();

            var selectedItem = SelectedItem;
            await detail.Delete();

            if (selectedItem != null)
                Items.Remove(selectedItem);

            SelectedItem = null;
            DetailItem = null;
        }

        public void NewEvent()
        {
            SelectedItem = null;
            DetailItem = new EventDetailViewModel(new Event(), true);
        }

        public void RevertEvent()
        {
            if (DetailItem.IsNew)
            {
                DetailItem = null;
            }
            else
            {
                DetailItem.LoadDataModel();
            }
        }

        private bool overviewEdit = false;

        public bool OverviewEdit
        {
            set
            {
                overviewEdit = value;

                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        item.EditMode = value;
                    }
                }

            }

            get
            {
                return overviewEdit;
            }
        }
    }
}
