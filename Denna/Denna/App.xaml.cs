using Core.Infrastructure;
using Denna.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Windows.UI.Popups;
using Microsoft.AppCenter.Crashes;

namespace Denna
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledException;
            AppCenter.Start("2b144adb-5aac-4c75-a761-5f9ee1a0fd92", typeof(Analytics));
            AppCenter.Start("2b144adb-5aac-4c75-a761-5f9ee1a0fd92", typeof(Crashes));
            Themesetter();
            DI.Build();
        }

        private async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await new MessageDialog(e.Exception.StackTrace, e.Exception.Message).ShowAsync();
            Analytics.TrackEvent("Crash happened");
        }

        private void stuff()
        {
            try
            {
                if (ApplicationData.Current.LocalSettings.Values["FollowAccent"] == null)
                    ApplicationData.Current.LocalSettings.Values["FollowAccent"] = false;
                if (Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values["FollowAccent"]) != true)
                    App.Current.Resources["SystemAccentColor"] = Windows.UI.Color.FromArgb(255, 32, 200, 165);
            }
            catch
            { }
        }
        void Themesetter()
        {
            if (Planel.Classes.Themesetter.GetApplicationTheme() != "System")
            {
                if (Planel.Classes.Themesetter.GetApplicationTheme() == "Dark")
                {
                    App.Current.RequestedTheme = ApplicationTheme.Dark;
                }
                else
                {
                    App.Current.RequestedTheme = ApplicationTheme.Light;
                }
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            stuff();
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                bool loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
                ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();
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
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
