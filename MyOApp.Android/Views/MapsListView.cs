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
    [Activity(Label = "Karten")]
    public class  MapsListView : MvxActivity
    {
        public new MapsListViewModel ViewModel
        {
            get { return (MapsListViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.MapsListView);
        }
    }
}