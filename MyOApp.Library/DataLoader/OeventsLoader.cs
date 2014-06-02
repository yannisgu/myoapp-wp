using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyOApp.Library.Models;
using MyOApp.Library.ViewModels;
using Newtonsoft.Json;

namespace MyOApp.Library.DataLoader
{
    public class OeventsLoader
    {
        string BASE_URL = "http://oevents.aws.af.cm/";

        public async Task<List<Event>> GetEvents(long lastModification = 0)
        {
            var httpClient = new HttpClient();
          
            var query =  String.Format(@"{{""source"": ""solv"", ""lastModification"": {{""$gt"": {0}}}}}", lastModification);

            var url = BASE_URL + "events?" + Uri.EscapeDataString(query);


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request);
            
            var events = JsonConvert.DeserializeObject<Event[]>( await response.Content.ReadAsStringAsync());
            return events.ToList();
        }

        public async Task LoadEvents(long lastModfication, ObservableCollection<EventItemViewModel> viewModels )
        {
            var dataAcces = Platform.DataAccess;
            var oevents = await GetEvents(lastModfication);
            var currentEvents = await dataAcces.GetEvents();
            foreach (var ev in oevents)
            {
                var oldEvent = currentEvents.FirstOrDefault(e => e.SourceId == ev.SourceId);
                if (oldEvent != null)
                {
                    ev.Id = oldEvent.Id;
                    ev.Selected = oldEvent.Selected;
                    viewModels[viewModels.IndexOf(viewModels.FirstOrDefault(m => m.Id == oldEvent.Id))] = new EventItemViewModel(ev);
                    await dataAcces.UpdateEvent(ev);
                }
                else
                {
                    ev.Selected = true;
                    await dataAcces.UpdateEvent(ev);
                    var previous = viewModels.FirstOrDefault(m => m.Date > ev.Date);
                    
                    var viewModel = new EventItemViewModel(ev);
                    if (previous == null)
                    {
                        viewModels.Add(viewModel);
                    }
                    else
                    {
                        viewModels.Insert(viewModels.IndexOf(previous), viewModel);
                    }
                }

            }
        }

    }
}
