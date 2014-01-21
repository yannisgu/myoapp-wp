using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyOApp.Phone.Resources;
using Windows.System;

namespace MyOApp.Phone
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = App.RootViewModel.DetailItem;
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox.SelectedItem == null)
            {
                return;
            }
            object tag = ((ListBoxItem)listBox.SelectedItem).Tag;
            string action = tag != null ? tag.ToString() : null;

            switch (action)
            {
                case "Map":
                        NavigationService.Navigate(new Uri("/MapsPage.xaml", UriKind.Relative));
                    break;
                case "Url":
                    var url = App.RootViewModel.DetailItem.Model.Url;
                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        await Launcher.LaunchUriAsync(new Uri(url));

                    }
                    break;

                case "Timetable":
                    var model = App.RootViewModel.DetailItem.Model;
                    var to = "Bern";
                   /* if (!string.IsNullOrEmpty(model.EventCenter))
                    {
                        to = "to=" + model.EventCenter;
                    }
                    else if (model.EventCenterLatitude > 0 && model.EventCenterLongitude > 0)
                    {
                        to = "toll=" + model.EventCenterLongitude + ',' + model.EventCenterLatitude;

                    }*/
                    var date = model.Date.Ticks / TimeSpan.TicksPerSecond;
                    var timetableUrl = "sbbmobileb2c://timetable?" + to + "&time=" + date + "&accessid=dm89518e7a4e0bcf670";
                    await Launcher.LaunchUriAsync(new Uri(timetableUrl));
                    break;
                case "Starlist":
                    await Launcher.LaunchUriAsync(new Uri(App.RootViewModel.DetailItem.Model.UrlStartlist.Replace("kind=all", "")));
                    break;
                case "Results":
                    await Launcher.LaunchUriAsync(new Uri(App.RootViewModel.DetailItem.Model.UrlResults.Replace("kind=all", "")));
                    break;
            }

            listBox.SelectedItem = null;
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}