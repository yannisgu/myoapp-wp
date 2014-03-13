using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using MyOApp.Library.Models;
using System;


namespace MyOApp.Library.ViewModels
{

    [Magic]
    public class EventItemViewModel : MvxViewModel
    {
        private readonly Event @event;

        public EventItemViewModel()
        {

        }

        public EventItemViewModel(Event selectedEvent)
        {
            @event = selectedEvent;
            LoadDataModel(selectedEvent);
        }

        public void LoadDataModel(Event model)
        {
            Id = model.Id;
            Name = model.Name;
            Date = model.Date;
            Map= model.Map;
            Organiser = model.Organiser;
            Region = model.Region;
            if (model.Selected != null)
            {
                Selected = (bool)model.Selected;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string MapDate
        {
            get
            {
                string value = "";
                if(!string.IsNullOrEmpty(Map))
                {
                    value += Map + ", ";
                }
                value += string.Format("{0:d}", Date);
                return value;
            }
        }

        public string RegionOrganiser
        {
            get
            {
                string value = "";

                value += Region;
                if(!string.IsNullOrEmpty(Region) && !string.IsNullOrEmpty(Organiser))
                {
                    value += ", ";
                }
                value += Organiser;
                return value;
            }
        }

        public string Map { get; set; }

        public string Region { get; set; }

        public string Organiser { get; set; }

        private bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                if (selected != @event.Selected)
                {
                    @event.Selected = selected;
                    Platform.DataAccess.UpdateEvent(@event);
                }
                RaisePropertyChanged("Selected");
                RaisePropertyChanged("IsVisible");
            }
        }

        private bool editMode;
        public bool EditMode
        {
            get
            {
                return editMode;
            }
            set
            {
                editMode = value;
                RaisePropertyChanged("EditMode");
                RaisePropertyChanged("IsVisible");
            }
        }
        public bool IsVisible
        {
            get
            {
                return EditMode || Selected;
            }
        }


        public Event Event
        {
            get { return @event; }
            
        }

    }
}
