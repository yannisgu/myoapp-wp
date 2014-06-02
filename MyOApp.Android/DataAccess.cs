using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Provider;
using Java.IO;
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
            var folderOld = System.IO.Path.Combine(folder, "../databases");
            if ((new DirectoryInfo(folderOld).Exists))
            {
                var  conn2 = new SQLiteConnection(System.IO.Path.Combine(folderOld, "_alloy_"));
                conn2.Execute("ALTER TABLE event RENAME TO old_event;");
                conn2.CreateTable<Event>(CreateFlags.AllImplicit | CreateFlags.AutoIncPK);
                conn2.Execute("INSERT INTO event(source_id, selected) SELECT source_id, enabled FROM old_event;");
                conn2.Close();
                conn2.Dispose();
                System.IO.File.Copy(System.IO.Path.Combine(folderOld, "_alloy_"),System.IO.Path.Combine(folder, "MyOApp.db"));

            }

            conn = new SQLiteConnection(System.IO.Path.Combine(folder, "MyOApp.db"));
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
