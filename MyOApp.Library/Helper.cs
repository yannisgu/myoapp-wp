using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOApp.Library
{
    public static class Helper
    {
        public static long GetTimestamp(DateTime dt)
        {

            long unixTimestamp = dt.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerMillisecond;
            return unixTimestamp;
        }
    }
}
