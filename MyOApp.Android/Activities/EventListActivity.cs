using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MyOApp.Android.Activities
{
    [Activity(Label = "MyOApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class EventListActivity : Activity
    {
        private ListView eventListView;
        private EventListAdapter eventListAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            eventListView = FindViewById<ListView>(Resource.Id.eventsListView);
            eventListAdapter = new EventListAdapter(this);
            eventListAdapter.DataBound += eventListAdapter_DataBound;
            eventListView.ItemClick+=eventListView_ItemClick;
            eventListView.Adapter = eventListAdapter;
            App.RootViewModel.PropertyChanged += RootViewModel_PropertyChanged;

        }

        void eventListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            App.RootViewModel.SelectedItem = App.RootViewModel.Items.FirstOrDefault(i => i.Id == e.Id);
        }


        void RootViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "DetailItem")
            {
                StartActivity(typeof(EventDetailActivity));
                //MainLongListSelector.SelectedItem = null;

            }
        }

        void eventListAdapter_DataBound(object sender, System.EventArgs e)
        {
            var scrollIndex =
                App.RootViewModel.Items.IndexOf(
                    App.RootViewModel.Items.FirstOrDefault(i => i.Date > DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0))));
            eventListView.SetSelection(scrollIndex);
        }


        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.RemoveItem(0);
            menu.Add(0, 0, 0, App.RootViewModel.OverviewEdit ? "Bearbeiten beenden" : "Bearbeiten");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0:
                    App.RootViewModel.OverviewEdit = !App.RootViewModel.OverviewEdit;
                    
                    eventListAdapter.NotifyDataSetChanged();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }    

        
    }
}

