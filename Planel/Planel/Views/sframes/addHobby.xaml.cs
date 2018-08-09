using Core.Models;
using Newtonsoft.Json;
using Planel.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
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
    public sealed partial class addHobby : Page
    {
        static ObservableCollection<Hobby> Hobbies = new ObservableCollection<Hobby>();
        public addHobby()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetUpPageAnimation();
            filler();
        }

        public void filler()
        {
            Hobbies = Core.Models.Localdb.Gethobbies();
            lvTest.ItemsSource = Hobbies;
        }

        void SetUpPageAnimation()
        {
            var collection = new TransitionCollection();

            var themeR = new EdgeUIThemeTransition();

            themeR.Edge = EdgeTransitionLocation.Bottom;

            collection.Add(themeR);
            Transitions = collection;
        }

        void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        async void lvTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as Core.Models.Hobby;
            var noWifiDialog = new ContentDialog()
            {
                Title = clk.title,
                Content = clk.detail + " at " + clk.time,
                PrimaryButtonText = "Ok"
            };

            var result = await noWifiDialog.ShowAsync();
        }

        void delete_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Are you sure?");
            msg.Commands.Add(new UICommand("Yes", async delegate
            {
                var clk = ((sender as Button).Tag) as Core.Models.Hobby;
                await Core.Models.Localdb.DeleteHobby(clk.Id);
                Hobbies.Remove(clk);
                await Classes.worker.refresher("Wall");
            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }

        void AppBarButton_Click_1(object sender, RoutedEventArgs e)
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
            var jdays = JsonConvert.SerializeObject(days);
            byte notifymode = 0;
            if (rbs.IsChecked == true)
                notifymode = 0;

            if (rbn.IsChecked == true)
                notifymode = 1;

            if (rba.IsChecked == true)
                notifymode = 2;

            var todate = new TimeSpan(timepic.Time.Hours, timepic.Time.Minutes, 0);
            var myhobby = new Hobby() { detail = describe.Text, title = title.Text, notify = notifymode, time = todate, Days = jdays };

            if (title.Text != "" && days.Count != 0)
            {
                if (Frame.CanGoBack)
                    Frame.GoBack();

                Core.Models.Localdb.Addhobby(myhobby);

                // HobbiesList.current.filler();
                Classes.worker.refresher("Add");
            }
            else
            {
                var messageDialog = new MessageDialog(MultilingualHelpToolkit.GetString("uphob", "Text"));
                messageDialog.ShowAsync();
            }
        }
    }
}