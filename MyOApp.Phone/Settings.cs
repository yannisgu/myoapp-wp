using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOApp.Phone
{
    public  class Settings
    {
        public  object this[string key]
        {
            get
            {
                object value = null;
                if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                {
                    value = IsolatedStorageSettings.ApplicationSettings[key];
                }
                return value;
            }
            set
            {
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (!settings.Contains(key))
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key] = value;
                }
                settings.Save();
            }
        }
    }
}
