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
    public sealed partial class Personalization : Page
    {
        public Personalization()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (AppSettings.Get<bool>("FollowAccent") == true)
                    FollowAccent.IsOn = true;
                else
                    FollowAccent.IsOn = false;
            }
            catch
            { }
            try
            {
                if (Classes.Themesetter.GetApplicationTheme() == "Dark")
                    ThemeSelector.SelectedIndex = 0;
                else if (Classes.Themesetter.GetApplicationTheme() == "Light")
                    ThemeSelector.SelectedIndex = 1;
                else
                    ThemeSelector.SelectedIndex = 2;
            }
            catch { }
            base.OnNavigatedTo(e);
        }
        void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.Themesetter.SetApplicationTheme((ThemeSelector.SelectedItem as ComboBoxItem).Tag.ToString());
            }
            catch { }
        }

        void FollowAccent_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FollowAccent.IsOn)
                    AppSettings.Set("FollowAccent", true);
                else
                    AppSettings.Set("FollowAccent", false);
            }
            catch
            {
            }
        }
    }
}
