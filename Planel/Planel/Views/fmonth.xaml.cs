using Core;
using Planel.Views.sframes;
using System;
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
        private ObservableCollection<Core.Models.todo> todolist = new ObservableCollection<Core.Models.todo>();
        public static fmonth current;
        public fmonth()
        {
            this.InitializeComponent();
            current = this;
            

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await filllist();
        }

        public async  Task filllist()
        {
            DateTime now = DateTime.Now;
            todolist = Core.Models.Localdb.Getfordoday(now);
            lvTest.ItemsSource = todolist;
        }
        private async void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {

            var clk = (Core.Models.todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            // await Models.Localdb.Deletetodo(clk.Id);

            await Core.Models.Localdb.Suspend(clk);
            await Classes.worker.refresher("Month");

        }

        private async void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (Core.Models.todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            await Core.Models.Localdb.Done(clk);
            await Classes.worker.refresher("Month");


        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(add));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private async void lvTest_ItemClick(object sender, ItemClickEventArgs e)
        {

            var clk = e.ClickedItem as Core.Models.todo;
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = clk.title,
                Content = clk.detail + " at " + clk.time,
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Are you sure?");
            msg.Commands.Add(new UICommand("Yes", async delegate {
                var clk = ((sender as Button).Tag) as Core.Models.todo;
                await Core.Models.Localdb.Deletetodo(clk.Id);
                todolist.Remove(clk);
                await Classes.worker.refresher("Month");
            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }
        private void MyCalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            
            try { 
            DateTime selecteddate = new DateTime(args.AddedDates[0].Year, args.AddedDates[0].Month, args.AddedDates[0].Day,0,0,0);

               todolist = Core.Models.Localdb.Getfordoday(selecteddate);
              lvTest.ItemsSource = todolist;

            }
            catch
            {

            }
        }
    }
}
