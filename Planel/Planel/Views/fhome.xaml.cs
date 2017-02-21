using Core;
using Planel.Classes;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class fhome : Page
    {
        public static fhome current;
        public fhome()
        {
            this.InitializeComponent();
            current = this;
            

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
             percentful();
        }
        public async Task percentful()
        {
            Core.Classes.mpercent percent = new Core.Classes.mpercent();
            percent = Core.Models.Localdb.percentage();
            settoday(percent);
            setyesterday(percent);
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("about");

        }

        private void settoday(Core.Classes.mpercent percent)
        {
            int percentage = percent.firstpercentage;
            todayper.Text = percentage.ToString() + "%";
            todaypie.Percentage = percentage;
            todaysus.Percentage = 100- percent.firstsuspend ;
            if (percentage == 100)
                todayword.Text = MultilingualHelpToolkit.GetString("Perfect", "Text");
            else if (percentage <= 99 && percentage >= 75)
                todayword.Text = MultilingualHelpToolkit.GetString("Great", "Text");
            else if (percentage <= 74 && percentage >= 40)
                todayword.Text = MultilingualHelpToolkit.GetString("Do", "Text");
            else if (percentage <= 39 && percentage >= 10)
                todayword.Text = MultilingualHelpToolkit.GetString("alot", "Text");
            else if (percentage <= 9 && percentage >= 0)
                todayword.Text = MultilingualHelpToolkit.GetString("addo", "Text");



        }
        private void setyesterday(Core.Classes.mpercent percent)
        {
            int percentage = percent.secondpercentage;
            yesterpie.Percentage = percentage;
            yesterpar.Text = percentage.ToString() + "%";
            yestersus.Percentage =100- percent.secondsuspend ;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("setting");
        }
    }
}
