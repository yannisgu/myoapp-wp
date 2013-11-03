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
using MyOApp.Library.ViewModels;

namespace MyOApp.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.RootViewModel;
            App.RootViewModel.PropertyChanged += RootPropertyChanged;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.RootViewModel.SelectedItem = null;
        }


        void RootPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DetailItem")
            {
                var detail = App.RootViewModel.DetailItem;
                if (detail == null) return;


                NavigationService.Navigate(new Uri("/DetailsPage.xaml" + (detail.IsNew ? "?" + Guid.NewGuid() : null), UriKind.Relative));
                MainLongListSelector.SelectedItem = null;
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector != null && MainLongListSelector.SelectedItem != null)
                App.RootViewModel.SelectedItem = (EventItemViewModel)MainLongListSelector.SelectedItem;
        }


        private void MainLongListSelector_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (((EventItemViewModel)e.Container.Content).Date > DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)))
            {
                MainLongListSelector.ScrollTo(e.Container.Content);
            }
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