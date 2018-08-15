using Core.Utils;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Support : Page
    {
        public Support()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "mailto:mohsens22@outlook.com?subject=DENNA_V2_FeedBack".OpenUrl();

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/insiders".OpenUrl();

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "https://t.me/joinchat/EJ9gLURDAcbOxpoIGSdD8g".OpenUrl();


        private void Button_Click_3(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "https://github.com/MahStudio/Denna/issues".OpenUrl();

        private void Button_Click_4(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "https://github.com/MahStudio/Denna/".OpenUrl();

        private void Button_Click_5(object sender, Windows.UI.Xaml.RoutedEventArgs e) => "http://paypal.me/mohsens22".OpenUrl();
    }
}
