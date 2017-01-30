using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views.sframes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class addHobby : Page
    {
        public addHobby()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetUpPageAnimation();
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
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            if (sat.IsChecked == true)
                days.Add(DayOfWeek.Saturday);
            if (sun.IsChecked == true)
                days.Add(DayOfWeek.Sunday);
            if (mon.IsChecked == true)
                days.Add(DayOfWeek.Monday);
            if (tue.IsChecked == true)
                days.Add(DayOfWeek.Tuesday);
            if (wed.IsChecked == true)
                days.Add(DayOfWeek.Wednesday);
            if (thu.IsChecked == true)
                days.Add(DayOfWeek.Thursday);
            if (fri.IsChecked == true)
                days.Add(DayOfWeek.Friday);
            string jdays = JsonConvert.SerializeObject(days);
            byte notifymode = 0;
            if (rbs.IsChecked == true)
                notifymode = 0;

            if (rbn.IsChecked == true)
                notifymode = 1;

            if (rba.IsChecked == true)
                notifymode = 2;

            TimeSpan todate = new TimeSpan(timepic.Time.Hours, timepic.Time.Minutes, 0);
            Hobby myhobby = new Hobby() {detail=describe.Text , title = title.Text , notify=notifymode , time= todate , Days = jdays};
            
            
            if (title.Text != "" && days.Count != 0)
            {
                if (Frame.CanGoBack)
                    Frame.GoBack();

                Core.Models.Localdb.Addhobby(myhobby);




                HobbiesList.current.filler();
                Classes.worker.refresher("Add");
            }
            else
            {
                var messageDialog = new MessageDialog("Fill the title and dayly repeat at least :)");
                messageDialog.ShowAsync();
            }
        }
    }
}
