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
    public sealed partial class ftoday : Page
    {
        private List<Models.todo> todolist = new List<todo>();
        public ftoday()
        {
            this.InitializeComponent();
            

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
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
            MainPage.current.ntonavigate();
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
