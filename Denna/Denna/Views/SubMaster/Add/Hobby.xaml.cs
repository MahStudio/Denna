using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster.Add
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Hobby : Page
    {
        public Hobby()
        {
            InitializeComponent();
        }

        void AppBarButton_Click(object sender, RoutedEventArgs e) => Frame.GoBack();
    }
}
