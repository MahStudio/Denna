﻿using Core;
using Core.Models;
using Newtonsoft.Json;
using Planel.Views.sframes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    public enum MyTemplates
    {
        DataTemplate1,
        DataTemplate2
    }
    public class DennaDataTemplateSelecotr : DataTemplateSelector
    {
        public DataTemplate DennaDataTemplate1Template { get; set; }
        public DataTemplate DennaDataTemplate2Template { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var message = item as MessageModel;
            if (message.MessageType.ToString() == "DataTemplate1")
                return DennaDataTemplate1Template;
            else
                return DennaDataTemplate2Template;
        }
    }

    public class MessageModel : INotifyPropertyChanged
    {
        private object rootobj { get; set; }
        private Enum _MessageType { get; set; }
        public object RootObject
        {
            get
            {
                return rootobj;
            }
            set
            {
                if (value != rootobj)
                {
                    rootobj = value;
                    OnPropertyChanged("RootObject");
                }
            }
        }
        public Enum MessageType
        {
            get
            {
                return _MessageType;
            }
            set
            {
                if (value != _MessageType)
                {
                    _MessageType = value;
                    OnPropertyChanged("MessageType");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ftoday : Page
    {
        private static ObservableCollection<Hobby> Hobbiese = new ObservableCollection<Hobby>();
        ObservableCollection<Core.Models.todo> todolist = new ObservableCollection<Core.Models.todo>();
        private static ObservableCollection<Hobby> toshow = new ObservableCollection<Hobby>();
        ObservableCollection<object> myobserv = new ObservableCollection<object>();
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
            myobserv.Clear();
            DateTime now = DateTime.Now;
            
            todolist = Core.Models.Localdb.getall(now);
            
            foreach (var item in todolist)
            {
                myobserv.Add(new MessageModel() { MessageType = MyTemplates.DataTemplate2, RootObject = item });
            }
            Hobbiese = Core.Models.Localdb.Gethobbies();
            await trim(Hobbiese);
            foreach (var item in Hobbiese)
            {
                myobserv.Add(new MessageModel() { MessageType = MyTemplates.DataTemplate1, RootObject = item });
            }
            Hobbies.ItemsSource = myobserv;
        }
        private static async Task trim(ObservableCollection<Hobby> stuff)
        {
            toshow.Clear();

            foreach (var item in stuff)
            {

                var a = DateTime.Now.DayOfWeek;
                var x = JsonConvert.DeserializeObject<List<DayOfWeek>>(item.Days);
                bool iscontain = _containstoday(x, a);
                if (iscontain)
                {

                    toshow.Add(item);

                }


            }
        }
        private static bool _containstoday(List<DayOfWeek> list, DayOfWeek day)
        {
            bool result = false;

            foreach (var item in list)
            {
                if (item == day)
                    result = true;
            }

            return result;
        }
        private async void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)((MessageModel)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext).RootObject;

            await Core.Models.Localdb.Suspend(clk);
            Classes.worker.refresher("Wall");

        }

        private async void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)((MessageModel)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext).RootObject;
            await Core.Models.Localdb.Done(clk);
            Classes.worker.refresher("Wall");


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
            msg.Commands.Add(new UICommand("Yes", async delegate
            {
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
            Frame.Navigate(typeof(addHobby));
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
            var dialog = new Windows.UI.Popups.MessageDialog("Sharelink copied in your clip board. ");
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

        private async void Hobbies_ItemClick(object sender, ItemClickEventArgs e)
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

        private void removehobbie_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Are you sure?");
            msg.Commands.Add(new UICommand("Yes", async delegate
            {
                var clk = ((sender as Button).Tag) as Core.Models.Hobby;
                await Core.Models.Localdb.DeleteHobby(clk.Id);

                myobserv.Remove(new MessageModel() { MessageType = MyTemplates.DataTemplate1, RootObject = clk });

            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }
    }
}
