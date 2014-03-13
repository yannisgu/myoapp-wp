using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.Models;
using System.Threading.Tasks;

namespace MyOApp.Library.ViewModels
{

    [Magic]
    public class EventDetailViewModel : MvxViewModel
    {
        Event model;
        public Event Model
        {
            get { return model; }
            set { model = value; }
        }


        public async Task LoadDataModel()
        {
            Name = model.Name;

            MapViewModel = new MapOverviewViewModel(Model.Map);
            await MapViewModel.LoadMaps();

        }


        public async void Init(Event @event)
        {
            Model = @event;
            await LoadDataModel();
        }

        public string Name { get; set; }

        public MapOverviewViewModel MapViewModel
        {
            get;
            set;
        }
    }
}
