
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        private DataTransferManager dataTransferManager;
        public About()
        {
            this.InitializeComponent();
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
             App_BackRequested;
            var v = Package.Current.Id.Version;
            ApplicationVersion.Text = "V" + string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);

        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }

        }

        ~About()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -=
             App_BackRequested;
        }
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            string myPages = "";
            foreach (PageStackEntry page in rootFrame.BackStack)
            {
                myPages += page.SourcePageType.ToString() + "\n";
            }


            if (rootFrame.CanGoBack)
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
            this.dataTransferManager = DataTransferManager.GetForCurrentView();
            this.dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // unregister as share source
            this.dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
        }
        private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            Uri dataPackageUri = new Uri("https://www.microsoft.com/store/apps/9n9c2hwnzcft");
            DataPackage requestData = e.Request.Data;
            requestData.Properties.Title = "DENNA | Plan your life !";
            requestData.SetWebLink(dataPackageUri);
            requestData.Properties.Description = "Check out the best todo manager app world wide  named DENNA ! Now in Windows Store.";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("ms-windows-store://review/?productid=9n9c2hwnzcft");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:mohsens22@outlook.com?subject=DENNA_BETA_FeedBack"));
        }
    }
}
