using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOApp.Library.Models
{
    public class MapItemModel
    {
        [JsonProperty("compname")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("imagelink")]
        public string ImageUrl
        {
            get;
            set;
        }


        public Uri ImageUri
        {
            get
            {
                return new Uri(ImageUrl);
            }
            
        }

        [JsonProperty("thumblink")]
        public string ThumbnailId
        {
            get;
            set;
        }

        public string Thumbnail
        {
            get
            {
                string baseUrl = "http://omaps.worldofo.com/images/{0}.jpg";

                return string.Format(baseUrl, ThumbnailId);
            }
        }


        public Uri ThumbnailUri
        {
            get
            {
                return new Uri(Thumbnail);
            }

        }

    }
}
