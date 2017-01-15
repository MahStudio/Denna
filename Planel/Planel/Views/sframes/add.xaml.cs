using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views.sframes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class add : Page
    {
        public add()
        {
            this.InitializeComponent();
            SetUpPageAnimation();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }
        private void SetUpPageAnimation()
        {
            TransitionCollection collection = new TransitionCollection();
            

            var themeR = new EdgeUIThemeTransition();

            themeR.Edge = EdgeTransitionLocation.Bottom;

            
            collection.Add(themeR);
            this.Transitions = collection;
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //ignore
            Frame.Navigate(typeof(ftoday));
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            //add to database
            DateTime todate = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes, timepic.Time.Seconds);
            byte notifymode = 0;
            if (rbs.IsChecked == true)
                notifymode = 0;

            if (rbn.IsChecked == true)
                notifymode = 1;

            if (rba.IsChecked == true)
                notifymode = 2;


            await Models.Localdb.Addtodo(title.Text, describe.Text, todate,notifymode);
            Classes.worker.refresher();

            Frame.Navigate(typeof(ftoday));
        }
    }
}
