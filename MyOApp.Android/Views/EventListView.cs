using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using MyOApp.Library.ViewModels;

namespace MyOApp.Android.Views
{
    [Activity(Label = "MyOApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public    class EventListView : MvxActivity
    {
        public new EventListViewModel ViewModel
        {
            get { return (EventListViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.EventListView);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var returnValue = false;

            switch (item.ItemId)
            {
                case 0:
                    ViewModel.OverviewEdit = !ViewModel.OverviewEdit;
                    returnValue = true;
                    break;
            }


            return returnValue;
        }



        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.Clear();
            if (ViewModel.OverviewEdit)
            {
                menu.Add(0, 0, 0, "Bearbeiten beenden");
            }
            else
            {
                menu.Add(0, 0, 0, "Bearbeiten");
            }
            return true;
        }
    }
}