using Planel.Models;
using Planel.Views.sframes;
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
    public sealed partial class fmonth : Page
    {
        private List<Models.todo> todolist = new List<todo>();
        public fmonth()
        {
            this.InitializeComponent();
            

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DateTime now = DateTime.Now;
            todolist = Models.Localdb.Getfordoday(now);
            lvTest.ItemsSource = todolist;
        }
        private void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            Models.Localdb.Deletetodo(clk.Id);
        }

        private void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            Models.Localdb.Done(clk);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(add));
        }

        private void MyCalendarView_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {

            
          

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
