using MyOApp.Library.Models;
using System;


namespace MyOApp.Library.ViewModels
{
    public class EventItemViewModel : PropertyChangedBase
    {
        public EventItemViewModel(Event selectedEvent)
        {
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
                    value += Map += ", ";
                }
                value += Date.ToString("d");
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
    }
}
