using Core;
using Newtonsoft.Json;
using Planel.Views.sframes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class ftoday : Page
    {
        ObservableCollection<Core.Models.todo> todolist = new ObservableCollection<Core.Models.todo>();
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

        public async Task filllist()
        {
            DateTime now = DateTime.Now;
            todolist = Core.Models.Localdb.getall(now);
            lvTest.ItemsSource = todolist;
        }

        private async void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {

            var clk = (Core.Models.todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
          
            await Core.Models.Localdb.Suspend(clk);
            await Classes.worker.refresher("Wall");

        }

        private async void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (Core.Models.todo)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext;
            await Core.Models.Localdb.Done(clk);
            await Classes.worker.refresher("Wall");


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
                Content = clk.detail + " at " + clk.time  ,
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
                await Classes.worker.refresher("Wall");
            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HobbiesList));
        }

        private void more_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sharetodo_Click(object sender, RoutedEventArgs e)
        {
            string shareuri = "Denna://Share/todo?";
            var clk = ((sender as Button).Tag) as Core.Models.todo;
            string json = JsonConvert.SerializeObject(clk);
            shareuri += json;
            var dataPackage = new DataPackage();
            dataPackage.SetText(shareuri);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            var dialog = new Windows.UI.Popups.MessageDialog( "Sharelink copied in your clip board. ");
            dialog.ShowAsync();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            var clk = ((sender as Button).Tag) as Core.Models.todo;
            Frame.Navigate(typeof(EditTask), clk);
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("setting");
        }

        private void AppBarButton_Click_4(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("about");
        }
    }
}
