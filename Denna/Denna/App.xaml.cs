using Core;
using Core.Infrastructure;
using Denna.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

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
            InitializeComponent();
            Suspending += OnSuspending;
            UnhandledException += App_UnhandledException;
            AppCenter.Start(Constants.AppCenterSecret, typeof(Analytics));
            AppCenter.Start(Constants.AppCenterSecret, typeof(Crashes));
            AppCenter.Start(Constants.AppCenterSecret, typeof(Push));
            Themesetter();
            DI.Build();
            Analytics.TrackEvent("App Opened");
        }

        async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Analytics.TrackEvent("Crash happened");
            await new MessageDialog(e.Exception.StackTrace, e.Exception.Message).ShowAsync();
        }

        void stuff()
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
                var loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
                var extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
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
        void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}