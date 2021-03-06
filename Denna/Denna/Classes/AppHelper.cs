﻿using Core.Utils;
using Denna.Converters;
using Denna.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Denna.Classes
{
    public static class AppHelper
    {
        public static async void OnUnhandledException(Windows.UI.Xaml.UnhandledExceptionEventArgs e, string sender)
        {
            Analytics.TrackEvent("Unhandled Exception");
            //Analytics.
            EasClientDeviceInformation clientDeviceInformation = new EasClientDeviceInformation();
            string message =
                e.Exception.Message +
                Environment.NewLine +
                Environment.NewLine +
                e.Exception.StackTrace +
                Environment.NewLine +
                clientDeviceInformation.SystemManufacturer +
                Environment.NewLine +
                clientDeviceInformation.SystemProductName +
                Environment.NewLine +
                clientDeviceInformation.OperatingSystem +
                Environment.NewLine +
                clientDeviceInformation.SystemHardwareVersion +
                Environment.NewLine +
                clientDeviceInformation.SystemFirmwareVersion +
                Environment.NewLine +
                clientDeviceInformation.FriendlyName +
                Environment.NewLine +
                clientDeviceInformation.SystemSku;
            MessageDialog msg = new MessageDialog(message, "Somethig is fucking wrong, you gotta report this to developer");
            msg.Commands.Add(new UICommand("Report this to developer", async delegate
            {
                $"mailto:MahStudio@outlook.com?subject={Uri.EscapeDataString($"Exception happened in Denna {Extentions.GetApplicationVersion()}")}&body={Uri.EscapeDataString(message)}".OpenUrl();
            }));
            msg.Commands.Add(new UICommand("Copy to clipboard", delegate
            {
                DataPackage pkg = new DataPackage();
                pkg.SetText(message);
                Clipboard.SetContent(pkg);
            }));
            msg.Commands.Add(new UICommand("Open an issue in Github", delegate
            {
                DataPackage pkg = new DataPackage();
                pkg.SetText(message);
                Clipboard.SetContent(pkg);

                "https://github.com/MahStudio/Denna/issues/new".OpenUrl();
            }));
            await msg.ShowAsync();

        }
        public static void LaunchApplication(IActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                ApplyThemeSettings();
                SetUpVoiceCommends();
                bool loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated);
                ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen, loadState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();
        }
        public static async Task SetUpVoiceCommends()
        {
            if (AppSettings.Get<bool>("VCDPresent") == false || AppSettings.OpenGet("VCDPresent") == null)
            {
                try
                {
                    // Install the main VCD. 
                    StorageFile vcdStorageFile =
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
        }
        public static void ApplyThemeSettings()
        {
            try
            {
                string s = AppSettings.OpenGet("FollowAccent").ToString();

                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                {
                    AppSettings.Set("FollowAccent", false);
                    App.Current.Resources["SystemAccentColor"] = Windows.UI.Color.FromArgb(255, 32, 200, 165);
                }
                else if (s.ToLower() == "true")
                {
                    ///Do nothing
                }
                else if (s.ToLower() == "false")
                {
                    App.Current.Resources["SystemAccentColor"] = Windows.UI.Color.FromArgb(255, 32, 200, 165);
                }
                else
                {
                    //Needs to use custom shit
                    App.Current.Resources["SystemAccentColor"] = ColorFromHexStringConverter.GetColor(s);
                }

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
