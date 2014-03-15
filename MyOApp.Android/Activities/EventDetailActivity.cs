using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace MyOApp.Android.Activities
{
   [Activity(Label = "MyOApp.Android", Icon = "@drawable/icon")]
    public class EventDetailActivity : Activity
   {
       private TextView labelMap;
       private TextView labelDate;
       private TextView labelOrganisator;
       private TextView labelLocation;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.EventDetail);

            labelDate = FindViewById<TextView>(Resource.Id.labelDate);
            labelMap = FindViewById<TextView>(Resource.Id.labelMap);
            labelOrganisator = FindViewById<TextView>(Resource.Id.labelOrganisator);
            labelLocation = FindViewById<TextView>(Resource.Id.labelLocation);

            labelDate.Text = App.RootViewModel.DetailItem.Model.Date.ToString("d");
            labelMap.Text = App.RootViewModel.DetailItem.Model.Map;
            labelOrganisator.Text = App.RootViewModel.DetailItem.Model.Organiser;
            labelLocation.Text = App.RootViewModel.DetailItem.Model.EventCenter;

        }

   }
}
