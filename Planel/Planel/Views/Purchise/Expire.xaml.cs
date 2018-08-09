using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Expire : Page
    {
        public Expire()
        {
            InitializeComponent();
        }

        async void logout_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?productid=9n9c2hwnzcft"));
        }

        void Iran_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(IranBye));
    }
}
