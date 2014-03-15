using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.DataLoader;
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

        public MapsListViewModel MapViewModel
        {
            get;
            set;
        }

        public ICommand OpenMapsCommand
        {
            get
            {


                return new MvxCommand(async () =>
                {
                    
                    ShowViewModel<MapsListViewModel>(new MapsListParameters()
                    {
                        MapName = Model.Name
                    
                    });
                }); 
            }
        }
    }
}
