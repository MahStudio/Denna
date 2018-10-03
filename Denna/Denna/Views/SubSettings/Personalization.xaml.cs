using Core.Utils;
using System.Collections.ObjectModel;
using System.Linq;
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

        private ObservableCollection<string> colors = new ObservableCollection<string>() { "fad616", "f3d741", "20c8a5", "12a889", "50b9ff", "50b9ff", "505cff", "6b5cf8", "f85ca0", "ff2e7d", "e54747", "ff2451" };
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string s = AppSettings.OpenGet("FollowAccent").ToString();

                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                {
                    defaultColor.IsChecked = true;
                }
                else if (s.ToLower() == "true")
                {
                    Accent.IsChecked = true;
                }
                else if (s.ToLower() == "false")
                {
                    defaultColor.IsChecked = true;
                }
                else
                {
                    custom.IsChecked = true;
                }


            }
            catch
            { }
            try
            {
                if (Classes.Themesetter.GetApplicationTheme() == "Dark")
                {
                    ThemeSelector.SelectedIndex = 0;
                }
                else if (Classes.Themesetter.GetApplicationTheme() == "Light")
                {
                    ThemeSelector.SelectedIndex = 1;
                }
                else
                {
                    ThemeSelector.SelectedIndex = 2;
                }
            }
            catch { }
            base.OnNavigatedTo(e);
        }

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.Themesetter.SetApplicationTheme((ThemeSelector.SelectedItem as ComboBoxItem).Tag.ToString());
            }
            catch { }
        }

        private void defaultColor_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            colorpiker.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AppSettings.Set("FollowAccent", false);
        }

        private void Accent_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            colorpiker.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AppSettings.Set("FollowAccent", true);
        }

        private void custom_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            colorpiker.Visibility = Windows.UI.Xaml.Visibility.Visible;
            string s = AppSettings.OpenGet("FollowAccent").ToString();
            colorslist.SelectedItem = colors.Where(x => x == s);
        }

        private void colorslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = e.AddedItems.FirstOrDefault().ToString();
            AppSettings.Set("FollowAccent", s);
        }
    }
}
