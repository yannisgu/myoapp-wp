using MyOApp.Library.Models;
using System;


namespace MyOApp.Library.ViewModels
{
    public class EventItemViewModel : PropertyChangedBase
    {
        private Event model;

        public EventItemViewModel()
        {

        }

        public EventItemViewModel(Event selectedEvent)
        {
            model = selectedEvent;
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
            Selected = (bool)model.Selected;
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
                if (selected != model.Selected)
                {
                    model.Selected = selected;
                    Platform.DataAccess.UpdateEvent(model);
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

    }
}
