// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the EventListView.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using MyOApp.Library.ViewModels;

namespace MyOApp.Phone.Views
{
    /// <summary>
    ///    Defines the EventListView.xaml type.
    /// </summary>
    public partial class EventListView
    {
        private ApplicationBarIconButton appBarBtnEdit;
        private ApplicationBarIconButton appBarBtnConfirm;


        /// <summary>
        /// Initializes a new instance of the <see cref="EventListView"/> class.
        /// </summary>
        public EventListView()
        {
            this.InitializeComponent();
            BuildApplicationBar();
        }

        public new EventListViewModel ViewModel
        {
            get { return (EventListViewModel) base.ViewModel; }
            set { base.ViewModel = value; }
        }



        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ViewModel.OverviewEdit)
            {
                if (MainLongListSelector != null && MainLongListSelector.SelectedItem != null)
                {
                    ViewModel.SelectedItem = (EventItemViewModel)MainLongListSelector.SelectedItem;
                    ViewModel.DisplayDetailCommand.Execute(null);
                }
            }
        }

        private async void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            ViewModel.OverviewEdit = sender == appBarBtnEdit;
            ApplicationBar.Buttons.Clear();
            ApplicationBar.Buttons.Add(ViewModel.OverviewEdit ? appBarBtnConfirm : appBarBtnEdit);
        }

        private void MainLongListSelector_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (ViewModel.OverviewEdit && MainLongListSelector.SelectedItem != null)
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
            appBarBtnEdit = new ApplicationBarIconButton(new Uri("/Images/edit.png", UriKind.Relative))
            {
                Text = "bearbeiten"
            };
            appBarBtnEdit.Click += ApplicationBarIconButton_Click;
            ApplicationBar.Buttons.Add(appBarBtnEdit);

            // Create a new button and set the text value to the localized string from AppResources.
            appBarBtnConfirm = new ApplicationBarIconButton(new Uri("/Images/check.png", UriKind.Relative))
            {
                Text = "beenden"
            };
            appBarBtnConfirm.Click += ApplicationBarIconButton_Click;
        }

    }
}