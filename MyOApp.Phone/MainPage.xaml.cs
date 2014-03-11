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
using System.Threading;
using System.Threading.Tasks;

namespace MyOApp.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool scrolledToToday = false;
        private ApplicationBarIconButton appBarBtnEdit;
        private ApplicationBarIconButton appBarBtnConfirm;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.RootViewModel;
            App.RootViewModel.PropertyChanged += RootPropertyChanged;
            MainLongListSelector.Loaded += MainLongListSelector_Loaded;

            // Sample code to localize the ApplicationBar
            BuildApplicationBar();
        }

        void MainLongListSelector_Loaded(object sender, RoutedEventArgs e)
        {
            var items = MainLongListSelector.ItemsSource as IEnumerable<EventItemViewModel>;
            if (items != null)
            {
                var scrollToItem = items.FirstOrDefault(i => i.Date > DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)));
                if (scrollToItem != null)
                {
                   MainLongListSelector.ScrollTo(scrollToItem);
                }
            }
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.RootViewModel.SelectedItem = null;
        }


        async void RootPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            if (!App.RootViewModel.OverviewEdit)
            {
                if (MainLongListSelector != null && MainLongListSelector.SelectedItem != null)
                {
                    App.RootViewModel.SelectedItem = (EventItemViewModel)MainLongListSelector.SelectedItem;
                }
            }
        }

        private async void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.RootViewModel.OverviewEdit = sender == appBarBtnEdit;
            ApplicationBar.Buttons.Clear();
            ApplicationBar.Buttons.Add(App.RootViewModel.OverviewEdit ? appBarBtnConfirm : appBarBtnEdit);
        }

        private void MainLongListSelector_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (App.RootViewModel.OverviewEdit && MainLongListSelector.SelectedItem != null)
            {
                ((EventItemViewModel)MainLongListSelector.SelectedItem).Selected = !((EventItemViewModel)MainLongListSelector.SelectedItem).Selected;
                MainLongListSelector.SelectedItem = null;
            }
        }

        private void CheckBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            e.Handled = true;
        }

        private void BuildApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();
            

            // Create a new button and set the text value to the localized string from AppResources.
            appBarBtnEdit = new ApplicationBarIconButton(new Uri("/Images/edit.png", UriKind.Relative));
            appBarBtnEdit.Text = "bearbeiten";
            appBarBtnEdit.Click += ApplicationBarIconButton_Click;
            ApplicationBar.Buttons.Add(appBarBtnEdit);

            // Create a new button and set the text value to the localized string from AppResources.
            appBarBtnConfirm = new ApplicationBarIconButton(new Uri("/Images/check.png", UriKind.Relative));
            appBarBtnConfirm.Text = "beenden";
            appBarBtnConfirm.Click += ApplicationBarIconButton_Click;



        }
    }
}