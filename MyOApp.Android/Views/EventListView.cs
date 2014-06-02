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
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Views;
using MyOApp.Library.ViewModels;

namespace MyOApp.Android.Views
{
    [Activity(Label = "MyOApp", MainLauncher = true, Icon = "@drawable/icon")]
    public    class EventListView : MvxActivity
    {
        public new EventListViewModel ViewModel
        {
            get { return (EventListViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            ViewModel.ItemsLoaded += ViewModel_ItemsLoaded;
            SetContentView(Resource.Layout.EventListView);
        }

        void ViewModel_ItemsLoaded(object sender, EventArgs e)
        {
            var index = ViewModel.Items.IndexOf(ViewModel.Items.FirstOrDefault(d => d.Date > DateTime.Now.AddDays(-1)));
            if (index >= 0)
            {
                var listView = FindViewById<MvxListView>(Resource.Id.listView);

                listView.SetSelection(index);
            }
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var returnValue = false;

            switch (item.ItemId)
            {
                case Resource.Id.edit:
                    ViewModel.OverviewEdit = !ViewModel.OverviewEdit;
                    InvalidateOptionsMenu();
                    returnValue = true;
                    break;
            }


            return returnValue;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.eventListMenu,menu);
            return true;
            
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            var item = menu.FindItem(Resource.Id.edit);
            item.SetTitle(ViewModel.OverviewEdit ? "Bearbeiten beenden" : "Bearbeiten");
            return true;
        }
    }
}