using Denna.Views.SubSettings;
using PubSub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
            App_BackRequested;
            this.Subscribe<Classes.SetttingsHeader>(Text =>
            {
                Name.Text = Text.Text;
            });
        }
        ~Settings()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -=
             App_BackRequested;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {


            if (SFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (SFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                SFrame.GoBack();
                return;
            }
            else if (!SFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                Frame.GoBack();
            }


        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (SFrame == null || Frame == null || e.Handled)
                return;
            

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (SFrame.CanGoBack )
            {
                e.Handled = true;
                SFrame.GoBack();
                return;
            }
            else if (!SFrame.CanGoBack)
            {

                e.Handled = true;
                Frame.GoBack();
            }


        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SFrame.Navigate(typeof(Setting));
           
            if (Frame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

        }
    }
}
