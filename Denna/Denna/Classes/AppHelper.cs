using Autofac.Core;
using Core.Utils;
using Denna.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Denna.Classes
{
    public static class AppHelper
    {
        public static void LaunchApplication(IActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                ApplyThemeSettings();
                SetUpVoiceCommends();
                var loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
                var extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();
        }
        public static async Task SetUpVoiceCommends()
        {
            if (AppSettings.Get<bool>("VCDPresent") == false || AppSettings.OpenGet("VCDPresent") == null)
                try
                {
                    // Install the main VCD. 
                    var vcdStorageFile =
                      await Package.Current.InstalledLocation.GetFileAsync(@"XMLs\CortanaVCD.xml");

                    await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.
                      InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);
                    AppSettings.Set("VCDPresent", true);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
                }
        }
        public static void ApplyThemeSettings()
        {
            try
            {
                if (AppSettings.OpenGet("FollowAccent") == null)
                    AppSettings.Set("FollowAccent", false);
                if (Convert.ToBoolean(AppSettings.OpenGet("FollowAccent")) != true)
                    App.Current.Resources["SystemAccentColor"] = Windows.UI.Color.FromArgb(255, 32, 200, 165);
            }
            catch
            { }
        }

        public static void SetTheme()
        {
            if (Themesetter.GetApplicationTheme() != "System")
            {
                if (Themesetter.GetApplicationTheme() == "Dark")
                {
                    App.Current.RequestedTheme = ApplicationTheme.Dark;
                }
                else
                {
                    App.Current.RequestedTheme = ApplicationTheme.Light;
                }
            }
        }
    }
}
