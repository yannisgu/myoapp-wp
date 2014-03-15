using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Android.App;
using Android.Views;
using Android.Widget;
using MyOApp.Library;
using MyOApp.Library.DataLoader;
using MyOApp.Library.Models;

namespace MyOApp.Android
{
    public class EventListAdapter: BaseAdapter
    {
       // public delegate void DataBoundDelegate(object sender, EventArgs e);

        public event EventHandler DataBound;

        readonly Activity activity;
       
        public EventListAdapter(Activity activity)
        {
            this.activity = activity;
            LoadEvents();
        }

        public override int Count
        {
            get { return App.RootViewModel.Items != null ? App.RootViewModel.Items.Count : 0; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return App.RootViewModel.Items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = App.RootViewModel.Items[position];

            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.EventListItem, parent, false);
            var labelEventName = view.FindViewById<TextView>(Resource.Id.labelEventName);
            var labelEventRow2 = view.FindViewById<TextView>(Resource.Id.labelEventRow2);
            var labelEventRow3 = view.FindViewById<TextView>(Resource.Id.labelEventRow3);
            var checkbox = view.FindViewById<CheckBox>(Resource.Id.checkBoxEventSelected);

            labelEventName.Text = item.Name;
            labelEventRow2.Text = item.MapDate;
            labelEventRow3.Text = item.RegionOrganiser;
            checkbox.Visibility = item.EditMode ? ViewStates.Visible : ViewStates.Invisible;
            checkbox.Checked = item.Selected;
            checkbox.CheckedChange += (s, e) => item.Selected = e.IsChecked;

            return view;
        }

        async void LoadEvents()
        {
            await App.RootViewModel.LoadItems();
            try
            {

                await (new OeventsLoader()).LoadEvents(0, App.RootViewModel.Items);
            }
            catch (Exception ex)
            {
                
            }
            
            NotifyDataSetChanged();
            if (DataBound != null)
            {
                DataBound(this, new EventArgs());
            }
        }
    }
}
