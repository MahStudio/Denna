using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class fhome : Page
    {
        public fhome()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Classes.mpercent percent = new Classes.mpercent();
            percent = Models.Localdb.percentage();
            settoday(percent.firstpercentage);
            setyesterday(percent.secondpercentage);
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate();

        }

        private void settoday(int percentage)
        {
            todayper.Text= percentage.ToString() + "%";
            todaypie.Percentage = percentage;
            if (percentage == 100)
                todayword.Text = "Perfect! All done.";
            else if (percentage <=99 && percentage>=75)
                todayword.Text = "Great! Mostly done.";
            else if (percentage <= 74 && percentage >= 40)
                todayword.Text = "Do more :)";
            else if (percentage <= 39 && percentage >= 10)
                todayword.Text = "You have a lot to do";
            else if (percentage <= 9 && percentage >= 0)
                todayword.Text = "Come on! Do it!";



        }
        private void setyesterday(int percentage)
        {

            yesterpie.Percentage = percentage;
            yesterpar.Text = percentage.ToString() + "%";
        }


    }
}
