using System.Collections.Generic;
using System.Threading.Tasks;
using Lex.Db;
using MyOApp.Library;
using MyOApp.Library.Models;

namespace MyOApp.WindowsStore.DataAccess
{
    public class DataAccess : IDataAccess
    {
        DbInstance db;
        DbTable<Event> events;

        public DataAccess()
        {
            db = new DbInstance("EventsDB");

            db.Map<Event>().Automap(i => i.Id, true).WithIndex("Name", i => i.Name);

            db.Initialize();
            events = db.Table<Event>();
        }

        public DbTable<Event> Events { get { return events; } }

        public Task<Event[]> GetEvents()
        {
            return events.LoadAllAsync();
        }

        public Task UpdateEvent(Event Event)
        {
            return events.SaveAsync(Event);
        }

        public Task UpdateEvents(IEnumerable<Event> eventsToSave)
        {
            return events.SaveAsync(eventsToSave);
        }

        public Task<bool> DeleteEvent(Event Event)
        {
            return events.DeleteAsync(Event);
        }

        public Task<Event> GetEvent(int eventId)
        {
            return events.LoadByKeyAsync(eventId);
        }

        public Task<int> DeleteAllEvents()
        {
            return events.DeleteAsync(events.LoadAll());
        }
    }
}
