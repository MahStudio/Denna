using Denna.Views.SubSettings;
using Microsoft.AppCenter.Analytics;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
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
            InitializeComponent();

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
            App_BackRequested;
        }
        ~Settings()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -=
             App_BackRequested;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (Frame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (Frame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
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

            Analytics.TrackEvent("Settings Opened");
        }

        void ArtistsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as Classes.ItemHolder;
            switch (clk.ID)
            {
                case 1:
                    {
                        Frame.Navigate(typeof(Account));
                        break;
                    }
                case 3:
                    {
                        Frame.Navigate(typeof(Notifications));
                        break;
                    }
                case 4:
                    {
                        Frame.Navigate(typeof(QuickActions));
                        break;
                    }
                case 5:
                    {
                        Frame.Navigate(typeof(Personalization));
                        break;
                    }
                case 6:
                    {
                        Frame.Navigate(typeof(Language));
                        break;
                    }
                case 7:
                    {
                        Frame.Navigate(typeof(Support));
                        break;
                    }
                case 8:
                    {
                        Frame.Navigate(typeof(About));
                        break;
                    }
                default:
                    break;
            }
        }
    }
}