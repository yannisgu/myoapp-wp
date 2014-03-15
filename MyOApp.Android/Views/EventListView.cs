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
using Cirrious.MvvmCross.Droid.Views;
using MyOApp.Library.ViewModels;

namespace MyOApp.Android.Views
{
    public    class EventListView : MvxActivity
    {
        public new EventListViewModel ViewModel { get; set; }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.EventListView);
        }
    }
}