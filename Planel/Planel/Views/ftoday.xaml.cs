using Core;
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
    public class MessageDataTemplateSelecotr : DataTemplateSelector
    {
        public DataTemplate HobbieStyle { get; set; }
        public DataTemplate TaskStyle { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var message = item as MessageModel;
            if (message.MessageType.ToString() == "HobbieStyle")
                return HobbieStyle;
            else
                return TaskStyle;
        }
    }
    public class MessageModel : INotifyPropertyChanged
    {
        private Enum _MessageType;
        private int _id;
        private string _title;
        private string _detail;
        private DateTime _time;
        private byte _notify;
        private byte _isdone;
        private TimeSpan _Time;
        private string _weekdays;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
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
                    RaisePropertyChanged("MessageType");
                }
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
        public string title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged("title");
                }
            }
        }
        public string detail
        {
            get { return _detail; }
            set
            {
                if (_detail != value)
                {
                    _detail = value;
                    RaisePropertyChanged("detail");
                }
            }
        }
        public TimeSpan Time
        {
            get { return _Time; }
            set
            {
                if (_Time != value)
                {
                    _Time = value;
                    RaisePropertyChanged("Time");
                }
            }
        }
        public DateTime time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged("time");
                }
            }
        }
        
        //0 stands for no notify, 1 stands for toast notify and 2 stands for alarm
        public byte notify
        {
            get { return _notify; }
            set
            {
                if (_notify != value)
                {
                    _notify = value;
                    RaisePropertyChanged("notify");
                }
            }
        }
       
        // 0 stands for undone, 1 stands for suspend(snooze) and 2 stands for done
        public byte isdone
        {
            get { return _isdone; }
            set
            {
                if (_isdone != value)
                {
                    _isdone = value;
                    RaisePropertyChanged("isdone");
                }
            }
        }
        public string Days
        {
            get { return _weekdays; }
            set
            {
                if (_weekdays != value)
                {
                    _weekdays = value;
                    RaisePropertyChanged("Days");
                }
            }
        }
    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ftoday : Page
    {
        private static ObservableCollection<Hobby> Hobbiese = new ObservableCollection<Hobby>();
        ObservableCollection<Core.Models.todo> todolist = new ObservableCollection<Core.Models.todo>();
        private static ObservableCollection<MessageModel> listViewCollection = new ObservableCollection<MessageModel>();
        public static ftoday current;
        private enum _MessageType
        {
            TaskStyle,
            HobbieStyle
        }
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
            
            Hobbiese = Core.Models.Localdb.Gethobbies();
            await trim(Hobbiese);
            foreach (var item in Hobbiese)
            {

            }
        }
        private static async Task trim (ObservableCollection<Hobby> stuff)
        {
            listViewCollection.Clear();
            
            foreach (var item in stuff)
            {
                
                var a = DateTime.Now.DayOfWeek;
               var x=  JsonConvert.DeserializeObject<List<DayOfWeek>>(item.Days);
                bool iscontain = _containstoday(x, a);
                if (iscontain)
                {

                    listViewCollection.Add(new MessageModel() {

                        Days=item.Days , detail=item.detail , Id=item.Id, MessageType=_MessageType.HobbieStyle ,
                        Time=item.time , title=item.title
                    }
                        );
                    
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
            msg.Commands.Add(new UICommand("Yes", async delegate {
                var clk = ((sender as Button).Tag) as Core.Models.Hobby;
                await Core.Models.Localdb.DeleteHobby(clk.Id);
                Hobbiese.Remove(clk);
                await Classes.worker.refresher("Wall");
            }));
            msg.Commands.Add(new UICommand("Nope"));
            msg.ShowAsync();
        }
    }
}
