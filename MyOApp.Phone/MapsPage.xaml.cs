using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyOApp.Library.ViewModels;
using Windows.System;

namespace MyOApp.Phone
{
    public partial class MapsPage : PhoneApplicationPage
    {
        public MapsPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            
             App.RootViewModel.DetailItem.MapViewModel.LoadMaps();
            DataContext = App.RootViewModel.DetailItem.MapViewModel;
        }

        private async void mapsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (MapItemViewModel)MapsList.SelectedItem;

            await Launcher.LaunchUriAsync(new Uri(selectedItem.ImageUrl));
            
        }

    }
}