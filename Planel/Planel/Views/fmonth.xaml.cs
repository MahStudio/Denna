using Planel.Models;
using Planel.Views.sframes;
using System;
using System.Collections.ObjectModel;
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
    public sealed partial class fmonth : Page
    {
        private ObservableCollection<Models.todo> todolist = new ObservableCollection<Models.todo>();
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
            todolist = Models.Localdb.getall(now);
            lvTest.ItemsSource = todolist;
        }
        private async void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {

            var clk = (todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            // await Models.Localdb.Deletetodo(clk.Id);

            await Models.Localdb.Suspend(clk);

        }

        private async void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            await Models.Localdb.Done(clk);


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

            var clk = e.ClickedItem as todo;
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
            var clk = ((sender as Button).Tag) as todo;
            await Models.Localdb.Deletetodo(clk.Id);
        }
        private void MyCalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            
            try { 
            DateTime selecteddate = new DateTime(args.AddedDates[0].Year, args.AddedDates[0].Month, args.AddedDates[0].Day,0,0,0);

               todolist = Models.Localdb.Getfordoday(selecteddate);
              lvTest.ItemsSource = todolist;

            }
            catch
            {

            }
        }
    }
}
