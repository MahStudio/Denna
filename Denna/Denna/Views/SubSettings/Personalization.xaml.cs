
using Denna.Classes;
using PubSub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Personalization : Page
    {
        public Personalization()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.Publish(new Classes.SetttingsHeader("Personalization"));
            try
            {
                if (Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values["FollowAccent"]) == true)
                    FollowAccent.IsOn = true;
                else
                    FollowAccent.IsOn = false;
            }
            catch
            { }
            try
            {
                if (Themesetter.GetApplicationTheme() == "Dark")
                    ThemeSelector.SelectedIndex = 0;
                else if (Themesetter.GetApplicationTheme() == "Light")
                    ThemeSelector.SelectedIndex = 1;
                else
                    ThemeSelector.SelectedIndex = 2;
            }
            catch { }
        }


        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.Themesetter.SetApplicationTheme((ThemeSelector.SelectedItem as ComboBoxItem).Tag.ToString());
            }
            catch { }
        }

        private void FollowAccent_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FollowAccent.IsOn)
                    ApplicationData.Current.LocalSettings.Values["FollowAccent"] = true;
                else
                    ApplicationData.Current.LocalSettings.Values["FollowAccent"] = false;
            }
            catch
            {

            }

        }
    }
}
