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
        DataTransferManager dataTransferManager;
        public About()
        {
            InitializeComponent();
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
             App_BackRequested;
            var v = Package.Current.Id.Version;
            ApplicationVersion.Text = "V" + string.Format("{0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
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
        void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
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
            var rootFrame = Window.Current.Content as Frame;

            var myPages = "";
            foreach (PageStackEntry page in rootFrame.BackStack)
                myPages += page.SourcePageType.ToString() + "\n";


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

            dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(DataRequested);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // unregister as share source
            dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(DataRequested);
        }

        void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            var dataPackageUri = new Uri("https://www.microsoft.com/store/apps/9n9c2hwnzcft");
            var requestData = e.Request.Data;
            requestData.Properties.Title = "DENNA | Plan your life !";
            requestData.SetWebLink(dataPackageUri);
            requestData.Properties.Description = "Check out the best todo manager app world wide  named DENNA ! Now in Windows Store.";
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("ms-windows-store://review/?productid=9n9c2hwnzcft");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            // {
            //    var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
            //    await launcher.LaunchAsync();
            // }
            // else
            // {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:mohsens22@outlook.com?subject=DENNA_BETA_FeedBack"));
            // }
        }

        async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:mohsens22@outlook.com?subject=DENNA_BETA_Insider"));
        }
    }
}