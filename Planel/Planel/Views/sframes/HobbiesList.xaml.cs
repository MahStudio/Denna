using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views.sframes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HobbiesList : Page
    {
        private static ObservableCollection<Hobby> Hobbies = new ObservableCollection<Hobby>();
        public static HobbiesList current;
        public HobbiesList()
        {
            this.InitializeComponent();
            current = this;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            filler();
        }
        public void filler()
        {
            Hobbies = Core.Models.Localdb.Gethobbies();
            lvTest.ItemsSource = Hobbies;
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(addHobby));
        }

        private async void lvTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as Core.Models.Hobby;
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = clk.title,
                Content = clk.detail + " at " + clk.time,
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Are you sure?");
            msg.Commands.Add(new UICommand("Yes", async delegate {
                var clk = ((sender as Button).Tag) as Core.Models.Hobby;
                await Core.Models.Localdb.DeleteHobby(clk.Id);
                Hobbies.Remove(clk);
                await Classes.worker.refresher("Wall");
            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }
    }
}
