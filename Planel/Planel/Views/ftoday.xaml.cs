using Planel.Models;
using Planel.Views.sframes;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ftoday : Page
    {
        ObservableCollection<Models.todo> todolist = new ObservableCollection<todo>();
        public static ftoday current;
        public ftoday()
        {
            this.InitializeComponent();
            current = this;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            filllist();
        }

        public void filllist()
        {
            DateTime now = DateTime.Now;
            todolist = Models.Localdb.getall(now);
            lvTest.ItemsSource = todolist;
        }

        private void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {

            var clk = (todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            Models.Localdb.Deletetodo(clk.Id);
            
            todolist.Remove(clk);
            lvTest.ItemsSource = todolist;

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

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private async void lvTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as todo;
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = clk.title,
                Content = clk.detail + " at " + clk.time  ,
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
