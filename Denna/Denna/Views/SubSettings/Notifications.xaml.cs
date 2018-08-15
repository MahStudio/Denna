using Core.Service.Notifications;
using Core.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Notifications : Page
    {
        BackgroundService _service;
        public Notifications()
        {
            _service = new BackgroundService();
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (AppSettings.Get<bool>("Showtoast") == true)
                swicher.IsOn = true;
            else
                swicher.IsOn = false;

            base.OnNavigatedTo(e);
        }
        void swicher_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    AppSettings.Set("Showtoast", true);
                    _service.GenerateQuickAction();
                }
                else
                    AppSettings.Set("Showtoast", false);

            }
        }
    }
}
