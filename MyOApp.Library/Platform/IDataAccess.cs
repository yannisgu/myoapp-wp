using MyOApp.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOApp.Library
{
  public interface IDataAccess
  {
    Task<Event[]> GetEvents();
    Task UpdateEvent(Event item);
    Task<bool> DeleteEvent(Event item);
    Task<int> DeleteAllEvents();
    Task<Event> GetEvent(int eventId);
  }
}
