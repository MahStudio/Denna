using Core.Utils;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubSettings
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
            var v = Package.Current.Id.Version;
            ApplicationVersion.Text = "V" + string.Format("{0}.{1}.{2}.{3} Preview", v.Major, v.Minor, v.Build, v.Revision);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
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
        private void Image_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) => "https://mahstudio.github.io/".OpenUrl();

        private void Image_Tapped_1(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) => "https://realm.io/".OpenUrl();

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "https://github.com/MahStudio/Denna".OpenUrl();

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "ms-windows-store://review/?productid=9n9c2hwnzcft".OpenUrl();

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e) => DataTransferManager.ShowShareUI();
    }
}
