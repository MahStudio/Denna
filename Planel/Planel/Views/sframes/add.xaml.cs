using Newtonsoft.Json;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views.sframes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class add : Page
    {
        public add()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetUpPageAnimation();
            base.OnNavigatedTo(e);
            if (e.Parameter is Uri)
            {
                string str = e.Parameter.ToString();
                str = str.Remove(0,19);
                var toadd = JsonConvert.DeserializeObject<Core.Models.todo>(str);
                Core.Models.todo addr = new Core.Models.todo() { notify=toadd.notify , time = toadd.time, title=toadd.title , detail=toadd.detail};
                 
                MessageDialog msg = new MessageDialog("Do you wanna restore this backup ?");
                msg.Commands.Add(new UICommand("Yes", async delegate {
                    Core.Models.Localdb.Addtodo(addr);
                    Classes.worker.refresher("Add");
                    ContentDialog noWifiDialog = new ContentDialog()
                    {
                        Title = "Success!",
                        Content = "The shared todo had been saved",
                        PrimaryButtonText = "Nice!"
                    };
                    await noWifiDialog.ShowAsync();
                }));
                msg.Commands.Add(new UICommand("Nope"));
                msg.ShowAsync();


            }

        }
        private void SetUpPageAnimation()
        {
            TransitionCollection collection = new TransitionCollection();
            

            var themeR = new EdgeUIThemeTransition();

            themeR.Edge = EdgeTransitionLocation.Bottom;

            
            collection.Add(themeR);
            this.Transitions = collection;
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //ignore
            Frame.Navigate(typeof(ftoday));
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (title.Text != "")
            {
                if (Frame.CanGoBack)
                    Frame.GoBack();

                //add to database
                DateTime todate = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes, timepic.Time.Seconds);
                byte notifymode = 0;
                if (rbs.IsChecked == true)
                    notifymode = 0;

                if (rbn.IsChecked == true)
                    notifymode = 1;

                if (rba.IsChecked == true)
                    notifymode = 2;
                Core.Models.todo item = new Core.Models.todo() { detail = describe.Text, title = title.Text, time = todate, notify = notifymode, isdone = 0 };


                await Core.Models.Localdb.Addtodo(item);
                 Classes.worker.refresher("Add");
            }
            else
            {
                var messageDialog = new MessageDialog("Fill the title at least :)");
                messageDialog.ShowAsync();
            }
            
        }
    }
}
