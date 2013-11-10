using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOApp.Library.Models
{
    public class Event
    {
        public int Id
        {
            get;
            set;
        }

       [JsonPropertyAttribute("id")]
        public string SourceId { get; set; }

        public string Name
        {
            get;
            set;
        }


        private DateTime date;

        [JsonPropertyAttribute("date")]
        public long UnixTimestamp
        {
            get
            {
                return Helper.GetTimestamp(Date);
            }
            set
            {
                DateTime unixYear0 = new DateTime(1970, 1, 1);
                long unixTimeStampInTicks = value * TimeSpan.TicksPerMillisecond;
               date = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);

            }
        }

         public DateTime Date
         {
             get
             {
                 return date;
             }
             set
             {
                 date = value;
             }
         }

        public string Url { get; set; }

        public string Map { get; set; }

        public string Region { get; set; }

        public string Organiser { get; set; }


        public int Classfication { get; set; }
        public string EventCenter { get; set; }
        public float EventCenterLatitude { get; set; }
        public float EventCenterLongitude { get; set; }
        public bool Day { get; set; }
        public bool Night { get; set; }

        public string DistanceType { get; set; }
        public string Discipline { get; set; }


        public bool? Selected { get; set; }
    }
}
