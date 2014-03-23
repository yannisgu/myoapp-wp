using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyOApp.Library;
using MyOApp.Library.Models;
using SQLite;

namespace MyOApp.Android
{
    public class DataAccess : IDataAccess
    {
        SQLiteConnection conn;
        
        public DataAccess()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            conn = new SQLiteConnection(System.IO.Path.Combine(folder, "MyOApp2.db"));
           
            conn.CreateTable<Event>(CreateFlags.AllImplicit | CreateFlags.AutoIncPK);
        }

        public TableQuery<Event> Events { get { return conn.Table<Event>(); } }

        public Task<Event[]> GetEvents()
        {
            var task = new Task<Event[]>(() => Events.ToArray());
            task.Start();
            return task;
        }

        public Task UpdateEvent(Event ev)
        {
            var task = new Task(() =>
            {
                if(ev.Id != 0)
                {
                    conn.Update(ev);
                }
                else
                {
                    conn.Insert(ev);
                }
            });
            task.Start();
            return task;
        }

        public Task UpdateEvents(IEnumerable<Event> eventsToSave)
        {
            var task = new Task(() =>
            {
                foreach (var @event in eventsToSave)
                {
                    conn.InsertOrReplace(@event);
                }
            }); 
            task.Start();
            return task;
        }

        public Task<bool> DeleteEvent(Event @event)
        {
            var task = new Task<bool>(() => conn.Delete(@event) > 0);
            task.Start();
            return task;
        }

        public Task<Event> GetEvent(int eventId)
        {
            var task = new Task<Event>(() => Events.FirstOrDefault(e => e.Id == eventId));
            task.Start();
            return task;
        }

        public Task<int> DeleteAllEvents()
        {
            var task = new Task<int>(() => conn.DeleteAll<Event>());
            task.Start();
            return task;
        }
    }
}
