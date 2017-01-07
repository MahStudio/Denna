
using CrossPieCharts.UWP.PieCharts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;
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
            await refe();
        }
        public async Task refe(){
            await Classes.worker.percentful();
            Classes.mpercent percent = new Classes.mpercent();
            percent = Models.Localdb.percentage();
            settoday(percent.firstpercentage);
            setyesterday(percent.secondpercentage);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("about");

        }

        private void settoday(int percentage)
        {
            todayper.Text= percentage.ToString() + "%";
            
            if (percentage == 100)
                todayword.Text = "Perfect! All done.";
            else if (percentage <=99 && percentage>=75)
                todayword.Text = "Great! Mostly done.";
            else if (percentage <= 74 && percentage >= 40)
                todayword.Text = "Do more :)";
            else if (percentage <= 39 && percentage >= 10)
                todayword.Text = "You have a lot to do";
            else if (percentage <= 9 && percentage >= 0)
                todayword.Text = "Add and DO !";



        }
        private void setyesterday(int percentage)
        {

            
            yesterpar.Text = percentage.ToString() + "%";
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("setting");
        }
    }
}
