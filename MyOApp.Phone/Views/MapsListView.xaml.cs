using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.System;
using Microsoft.Phone.Controls;
using MyOApp.Library.Models;
using MyOApp.Library.ViewModels;

namespace MyOApp.Phone.Views
{
    public partial class MapsListView
    {
        public MapsListView()
        {
            InitializeComponent();
        }

        public new MapsListViewModel ViewModel { get; set; }

        private async void mapsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Map)MapsList.SelectedItem;

            await Launcher.LaunchUriAsync(new Uri(selectedItem.ImageUrl));
            
        }

    }
}