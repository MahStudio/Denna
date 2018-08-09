﻿using Core.Models;
using Newtonsoft.Json;
using Planel.Classes;
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
        object rootobj { get; set; }
        Enum _MessageType { get; set; }
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

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ftoday : Page
    {
        static ObservableCollection<Hobby> Hobbiese = new ObservableCollection<Hobby>();
        ObservableCollection<Core.Models.todo> todolist = new ObservableCollection<Core.Models.todo>();
        static ObservableCollection<Hobby> toshow = new ObservableCollection<Hobby>();
        ObservableCollection<object> myobserv = new ObservableCollection<object>();
        public static ftoday current;
        public ftoday()
        {
            InitializeComponent();
            current = this;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) => filllist();

        public async Task filllist()
        {
            myobserv.Clear();
            var now = DateTime.Now;

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

        static async Task trim(ObservableCollection<Hobby> stuff)
        {
            toshow.Clear();

            foreach (var item in stuff)
            {
                var a = DateTime.Now.DayOfWeek;
                var x = JsonConvert.DeserializeObject<List<DayOfWeek>>(item.Days);
                var iscontain = _containstoday(x, a);
                if (iscontain)
                {
                    toshow.Add(item);
                }
            }
        }

        static bool _containstoday(List<DayOfWeek> list, DayOfWeek day)
        {
            var result = false;

            foreach (var item in list)
                if (item == day)
                    result = true;


            return result;
        }

        async void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)((MessageModel)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext).RootObject;

            await Core.Models.Localdb.Suspend(clk);
            Classes.worker.refresher("Wall");
        }

        async void SlidableListItem_LeftCommandRequested(object sender, EventArgs e)
        {
            var clk = (todo)((MessageModel)(sender as Microsoft.Toolkit.Uwp.UI.Controls.SlidableListItem).DataContext).RootObject;
            await Core.Models.Localdb.Done(clk);
            Classes.worker.refresher("Wall");
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

        async void delete_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog(MultilingualHelpToolkit.GetString("Shor", "Text"));
            msg.Commands.Add(new UICommand("Yes", delegate
           {
               var clk = ((sender as Button).Tag) as Core.Models.todo;
               Core.Models.Localdb.Deletetodo(clk.Id);
               var a = Hobbies.Items.IndexOf((sender as Button).DataContext);

               if (a != null)
                   myobserv.RemoveAt(a);
               Classes.worker.refresher("Wall");
           }));
            msg.Commands.Add(new UICommand("No"));
            msg.ShowAsync();
        }

        void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(addHobby));
        }

        void more_Click(object sender, RoutedEventArgs e)
        {
        }

        void sharetodo_Click(object sender, RoutedEventArgs e)
        {
            var shareuri = "Denna://Share/todo?";
            var clk = ((sender as Button).Tag) as Core.Models.todo;
            var json = JsonConvert.SerializeObject(clk);
            shareuri += json;
            var dataPackage = new DataPackage();
            dataPackage.SetText(shareuri);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            var dialog = new Windows.UI.Popups.MessageDialog(MultilingualHelpToolkit.GetString("CLipB", "Text"));
            dialog.ShowAsync();
        }

        void edit_Click(object sender, RoutedEventArgs e)
        {
            var clk = ((sender as Button).Tag) as Core.Models.todo;
            Frame.Navigate(typeof(EditTask), clk);
        }

        void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("setting");
        }

        void AppBarButton_Click_4(object sender, RoutedEventArgs e)
        {
            MainPage.current.ntonavigate("about");
        }

        void removehobbie_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog(MultilingualHelpToolkit.GetString("Shor", "Text"));
            msg.Commands.Add(new UICommand("Yes", delegate
            {
                var clk = ((sender as Button).Tag) as Core.Models.Hobby;
                var res1 = myobserv.Where(x => ((x as MessageModel).RootObject as Hobby).Id == clk.Id);

                var a = Hobbies.Items.IndexOf((sender as Button).DataContext);

                if (a != null)
                    myobserv.RemoveAt(a);
                Core.Models.Localdb.DeleteHobby(clk.Id);
                Classes.worker.refresher("Wall");
            }));
            msg.Commands.Add(new UICommand("No"));
            msg.ShowAsync();
        }

        async void Hobbies_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as MessageModel;
            try
            {
                var todo = clk.RootObject as todo;
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
            catch
            {
                var todo = clk.RootObject as Hobby;
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
                stackdn.Visibility = Visibility.Collapsed;
                stackday.Visibility = Visibility.Visible;
                stackdays.Text = Convert(todo.Days);
            }

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
    }
}