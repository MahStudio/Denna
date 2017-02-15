using Core.Models;
using Planel.Views;
using SQLite.Net;
using System;
using Microsoft.HockeyApp;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Store;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Planel
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
        /// 
        public static LicenseInformation License;
        public static ListingInformation Listing;
        public static bool licenseactive;
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            licenseactive = true;
            // license();
            if (Classes.Themesetter.GetApplicationTheme() != "System")
            {
                if (Classes.Themesetter.GetApplicationTheme() == "Dark")
                {
                    App.Current.RequestedTheme = ApplicationTheme.Dark;
                }
                else
                {
                    App.Current.RequestedTheme = ApplicationTheme.Light;
                }
            }

            Microsoft.HockeyApp.HockeyClient.Current.Configure("452a83706987457a966d88403a1d770e");
        }
        public static async void license()
        {
            try
            {
                Listing = await CurrentApp.LoadListingInformationAsync();
            }
            catch (Exception ex)
            {
                try
                {
                    
                    //var proxyFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/trialmanagement.xml"));
                    //await Windows.ApplicationModel.Store.CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);
                  //  License = CurrentAppSimulator.LicenseInformation;
                  //  Listing = await CurrentAppSimulator.LoadListingInformationAsync();
                    
                }
                catch { }

                
            }
            
            
          

            if (ApplicationData.Current.LocalSettings.Values["LicenceActive"] == null )
            ApplicationData.Current.LocalSettings.Values["LicenceActive"] =false;

            
            licenseactive = (bool) ApplicationData.Current.LocalSettings.Values["LicenceActive"];
            try
            {
                if(License != null)
                {
                    if (License.IsActive == true || licenseactive == true)
                    {
                        licenseactive = true;

                    }
                    else
                    {
                        licenseactive = false;
                    }
                }
                else
                {
                    licenseactive = true;
                }
               
            }
            catch
            {
                licenseactive = true;
            }
                

            
            
            
            






        }
        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            base.OnFileActivated(args);
            coloradjust();
            var rootFrame = new Frame();
           // if (licenseactive == false && License.IsTrial == false)
            //    rootFrame.Navigate(typeof(Expire), args);

            if (ApplicationData.Current.LocalSettings.Values["Firstrun"] as string == "1")
            {
                rootFrame.Navigate(typeof(MainPage), args);
                migratedata();
            }
                
            else
                rootFrame.Navigate(typeof(WelcomePage), args);
            
            
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
        private async void SetUpVoiceCommends()
        {
            try
            {
                // Install the main VCD. 
                StorageFile vcdStorageFile =
                  await Package.Current.InstalledLocation.GetFileAsync(@"CortanaVCD.xml");

                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.
                  InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            SetUpVoiceCommends();
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {

                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    coloradjust();
                 //   if (licenseactive == false && License.IsTrial == false)
                    //    rootFrame.Navigate(typeof(Expire), e.Arguments);


                   if (ApplicationData.Current.LocalSettings.Values["Firstrun"] as string == "1") { 
                        rootFrame.Navigate(typeof(MainPage), e.Arguments);
                        migratedata();
                    }

                    else
                        rootFrame.Navigate(typeof(WelcomePage), e.Arguments);

                   
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }
        private void migratedata()
        {
            //var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            //using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            //{
            //    var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Hobby';";
            //    var result = conn.ExecuteScalar<string>(tableExistsQuery);
            //    if (result == null)
            //    {
            //        conn.CreateTable<Hobby>();
            //    }


            //}
        }
        
        
        private void coloradjust()
        {
            //try
            //{
            //    ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            //    titleBar.BackgroundColor = ((Color)Application.Current.Resources["SystemAccentColor"]);
            //    titleBar.ForegroundColor = Colors.White;
            //    titleBar.ButtonBackgroundColor = ((Color)Application.Current.Resources["SystemAccentColor"]);
            //    titleBar.ButtonForegroundColor = Colors.White;
            //    titleBar.InactiveBackgroundColor= ((Color)Application.Current.Resources["SystemAccentColor"]);
            //    titleBar.ButtonInactiveBackgroundColor= ((Color)Application.Current.Resources["SystemAccentColor"]);
            //    titleBar.InactiveForegroundColor= Colors.White;
            //    titleBar.ButtonInactiveForegroundColor= Colors.White;
            //    titleBar.ForegroundColor = Colors.White;
            //    titleBar.ButtonForegroundColor = Colors.White;
            //    titleBar.ButtonForegroundColor = Colors.White;
            //    //fuck you asshilism

            //}
            //catch
            //{

            //}
            //if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    var statusBar = StatusBar.GetForCurrentView();
            //    if (statusBar != null)
            //    {
            //        statusBar.BackgroundOpacity = 1;
            //        statusBar.BackgroundColor = ((Color)Application.Current.Resources["SystemAccentColor"]);
            //        statusBar.ForegroundColor = Colors.White;
            //    }
            //}

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
        protected override void OnActivated(IActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            
            


            



                
                base.OnActivated(args);

              
                
               if (args.Kind == ActivationKind.Protocol)
                {

                license();
                var protocolArgs = (ProtocolActivatedEventArgs)args;
                var uri = protocolArgs.Uri;
                if (rootFrame == null)
                {
                    rootFrame = new Frame();
                    rootFrame.NavigationFailed += OnNavigationFailed;
                    Window.Current.Content = rootFrame;
                }
                coloradjust();
                //  if (licenseactive == false && License.IsTrial == false)
                //     rootFrame.Navigate(typeof(Expire), uri);
                string x = uri.ToString();
                
                if (ApplicationData.Current.LocalSettings.Values["Firstrun"] as string == "1")
                {
                    if (x.Contains("agsonCortana") == false)
                    {
                        rootFrame.Navigate(typeof(MainPage), uri);
                    }
                    else
                    {
                        rootFrame.Navigate(typeof(MainPage));
                    }
                    
                    migratedata();
                }

                else
                    rootFrame.Navigate(typeof(WelcomePage), uri);

                // Ensure the current window is active
                Window.Current.Activate();


            }
                else
                {
                if (rootFrame == null)
                {
                    rootFrame = new Frame();
                    rootFrame.NavigationFailed += OnNavigationFailed;
                    Window.Current.Content = rootFrame;
                }
                
                    coloradjust();
                    //if (licenseactive == false && License.IsTrial == false)
                    //    rootFrame.Navigate(typeof(Expire), args);


                    if (ApplicationData.Current.LocalSettings.Values["Firstrun"] as string == "1")
                    {
                        rootFrame.Navigate(typeof(MainPage), args);
                        migratedata();
                    }

                    else
                        rootFrame.Navigate(typeof(WelcomePage), args);

                    // Ensure the current window is active
                    Window.Current.Activate();
               
                

            }
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
