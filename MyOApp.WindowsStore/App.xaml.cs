using MyOApp.Library;
using MyOApp.Library.Models;
using MyOApp.Library.ViewModels;
using MyOApp.WindowsStore.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Split App template is documented at http://go.microsoft.com/fwlink/?LinkId=234228

namespace MyOApp.WindowsStore
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        public static readonly RootViewModel RootViewModel = new RootViewModel();

        static App()
        {
            Platform.DataAccess = new DataAccess();
        }

        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            UnhandledException += App_UnhandledException;
        }

        async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(e.Exception.Message + " " + e.Exception.StackTrace);
            await messageDialog.ShowAsync();
            e.Handled = true;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(SplitPage), e.Arguments);
            }


            var dataAccess = Platform.DataAccess as DataAccess;

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    var localSettings = ApplicationData.Current.LocalSettings;
                    long? last = localSettings.Values["lastUpdate"] as long?;
                    await (new OeventsLoader()).LoadEvents(last != null ? (long)last : 0);
                    localSettings.Values["lastUpdate"] = Helper.GetTimestamp(DateTime.Now);


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            await App.RootViewModel.LoadItems();

            // Ensure the current window is active
            Window.Current.Activate();

            SettingsPane settingsPane = SettingsPane.GetForCurrentView();

            settingsPane.CommandsRequested += (s, settingsEvent) =>
            {
                SettingsCommand settingsCommand = new SettingsCommand(
                  "ABOUT_ID",
                  "Datenschutz",
                  command =>
                  {
                      var flyout = new SettingsFlyout();
                      flyout.Title = "Datenschutz";


                      flyout.Content = new TextBlock()
                      {
                          Text = "MyOApp sammelt oder sendet keine persönlichen identifizierbaren Daten. Die Dienste welche MyOApp verwendet speichern oder verwenden keine persönlichen Informationen.",
                          TextAlignment = Windows.UI.Xaml.TextAlignment.Left,
                          TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                          FontSize = 14
                      };

                      flyout.Show();
                  }
                );
                settingsEvent.Request.ApplicationCommands.Add(settingsCommand);
            };
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}
