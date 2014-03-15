using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyOApp.Library.ViewModels;
using MyOApp.Library;

namespace MyOApp.Android
{
    class App
    {
        public static EventListViewModel RootViewModel { get; set; }

        static App ()
        {
            RootViewModel = new EventListViewModel();
            Platform.DataAccess = new DataAccess();
        }
    }
}