using Core.Models;
using Newtonsoft.Json;
using Planel.Classes;
using Planel.Views.sframes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class fmonth : Page
    {
        ObservableCollection<Core.Models.todo> todolist = new ObservableCollection<Core.Models.todo>();
        public static fmonth current;
        public fmonth()
        {
            InitializeComponent();
            current = this;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) => filllist();

        public async Task filllist()
        {
            var now = DateTime.Now;
            todolist = Core.Models.Localdb.Getfordoday(now);
            lvTest.ItemsSource = todolist;
        }

        async void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {
            var clk = (Core.Models.todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            // await Models.Localdb.Deletetodo(clk.Id);

            await Core.Models.Localdb.Suspend(clk);
            Classes.worker.refresher("Month");
        }

        async void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (Core.Models.todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            await Core.Models.Localdb.Done(clk);
            Classes.worker.refresher("Month");
        }

        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(add));
        }

        void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
        }

        async void lvTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as todo;
            try
            {
                var todo = clk as todo;
                title.Text = todo.title;
                if (todo.detail != "")
                {
                    destk.Visibility = Visibility.Visible;
                    stackdetal.Text = todo.detail;
                }
                else
                {
                    destk.Visibility = Visibility.Collapsed;
                }

                stacktime.Text = todo.time.ToString();
                stackdn.Visibility = Visibility.Visible;
                stackday.Visibility = Visibility.Collapsed;
                if (todo.isdone == 0)
                {
                    stackglp.Text = "";
                    stackdone.Text = MultilingualHelpToolkit.GetString("Planned", "Text");
                }
                else if (todo.isdone == 1)
                {
                    stackglp.Text = "";
                    stackdone.Text = MultilingualHelpToolkit.GetString("Suspended", "Text");
                }
                else if (todo.isdone == 2)
                {
                    stackglp.Text = "";
                    stackdone.Text = MultilingualHelpToolkit.GetString("Done", "Text");
                }
            }
            catch { }

            Tododatail.Visibility = Visibility.Visible;
            await Task.Delay(10);
            myStoryboard.Begin();
            opacitySb1.Begin();
        }

        public string Convert(object value)
        {
            var json = value.ToString();
            var toadd = JsonConvert.DeserializeObject<IList<DayOfWeek>>(json);
            var Days = "";
            if (toadd.Count == 7)
            {
                Days = MultilingualHelpToolkit.GetString("Everydat", "Text"); ;
            }
            else
            {
                foreach (var item in toadd)
                {
                    string x = null;
                    if (item == DayOfWeek.Friday)
                    {
                        x = MultilingualHelpToolkit.GetString("fri", "Content");
                    }

                    if (item == DayOfWeek.Saturday)
                    {
                        x = MultilingualHelpToolkit.GetString("sat", "Content");
                    }

                    if (item == DayOfWeek.Sunday)
                    {
                        x = MultilingualHelpToolkit.GetString("sun", "Content");
                    }

                    if (item == DayOfWeek.Monday)
                    {
                        x = MultilingualHelpToolkit.GetString("mon", "Content");
                    }

                    if (item == DayOfWeek.Tuesday)
                    {
                        x = MultilingualHelpToolkit.GetString("tue", "Content");
                    }

                    if (item == DayOfWeek.Wednesday)
                    {
                        x = MultilingualHelpToolkit.GetString("wed", "Content");
                    }

                    if (item == DayOfWeek.Thursday)
                    {
                        x = MultilingualHelpToolkit.GetString("thu", "Content");
                    }

                    Days += x + "  ";
                }
            }

            return Days;
        }

        async void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            opacitySb0.Begin();
            myStoryboard0.Begin();
            await Task.Delay(300);
            Tododatail.Visibility = Visibility.Collapsed;
        }

        async void delete_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog(MultilingualHelpToolkit.GetString("Shor", "Text"));
            msg.Commands.Add(new UICommand("Yes", async delegate
            {
                var clk = ((sender as Button).Tag) as Core.Models.todo;
                await Core.Models.Localdb.Deletetodo(clk.Id);
                todolist.Remove(clk);
                Classes.worker.refresher("Month");
            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }

        void MyCalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            try
            {
                var selecteddate = new DateTime(args.AddedDates[0].Year, args.AddedDates[0].Month, args.AddedDates[0].Day, 0, 0, 0);

                todolist = Core.Models.Localdb.Getfordoday(selecteddate);
                lvTest.ItemsSource = todolist;
            }
            catch
            {
            }
        }
    }
}