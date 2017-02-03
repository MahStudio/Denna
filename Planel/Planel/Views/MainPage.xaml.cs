using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static Planel.Classes.worker;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Point startpoint;
        Point lastPostition;
        public static MainPage current;
        bool isopen = false;
        public MainPage()
        {
            this.InitializeComponent();
            current = this;
            int runtime = (int)ApplicationData.Current.LocalSettings.Values["RunTime"];
            if (runtime <= 6)
            {
                mtoday();
                runtime++;
                ApplicationData.Current.LocalSettings.Values["RunTime"] = runtime;
            }
               

        }


        public  void messagesetter (string message)
        {
            news.Text = message;
        }
        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
                var req = await BackgroundExecutionManager.RequestAccessAsync();
                if (req != BackgroundAccessStatus.DeniedByUser && req != BackgroundAccessStatus.DeniedBySystemPolicy)
                {
                    var list = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "NotifierTask");
                    foreach (var item in list)
                    {
                        item.Value.Unregister(false);
                    }
                    BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder { Name = "NotifierTask", TaskEntryPoint = "NotifierTask.Notify" };
                    taskBuilder.SetTrigger(new TimeTrigger(15, false));
                    
                    BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
                    {
                        Name = "MyToastTask",
                        TaskEntryPoint = "Toaster.Toaster"
                    };

                    builder.SetTrigger(new ToastNotificationActionTrigger());
                    BackgroundTaskRegistration registration = builder.Register();
                    BackgroundTaskRegistration myFirstTask = taskBuilder.Register();
                
            }

            Frame rootFrame = Window.Current.Content as Frame;

            //string myPages = "";
            //foreach (PageStackEntry page in rootFrame.BackStack)
            //{
            //    myPages += page.SourcePageType.ToString() + "\n";
            //}


            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

            fhome.Navigate(typeof(fhome));
            ftoday.Navigate(typeof(ftoday));
            fmonth.Navigate(typeof(fmonth));
            fpref.Navigate(typeof(fpref));
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("avatar.jpg");

            avatar.ImageSource = new BitmapImage(new Uri(sampleFile.Path));
            DateTime thisday = DateTime.Today;
            todate.Text = thisday.ToString("D");
            counterr(Core.Models.Localdb.counter());
           
            Classes.worker.tiler();

            base.OnNavigatedTo(e);
            var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
            bool isloaded = Settings.isloaded;
            if (args != null && isloaded != true)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File )
                {
                    
                    Frame.Navigate(typeof(Settings), args);
                    

                }
            }
            else
            {
                Settings.isloaded = false;
            }
             if (e.Parameter is Uri)
            {
                mtoday();

                string str = e.Parameter.ToString();
                str = str.Remove(0, 19);
                var toadd = JsonConvert.DeserializeObject<Core.Models.todo>(str);
                Core.Models.todo addr = new Core.Models.todo() { notify = toadd.notify, time = toadd.time, title = toadd.title, detail = toadd.detail };

                MessageDialog msg = new MessageDialog("Do you wanna add this to your todos ?");
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

        public void counterr(int cnt)
        {



            if (cnt == 1 || cnt == 0)
                counter.Text = string.Format("You have {0} work to do", cnt.ToString());
            else
                counter.Text = string.Format("You have {0} works to do", cnt.ToString());

        }
        public void ntonavigate(string nav)
        {
            if (nav == "about")
                Frame.Navigate(typeof(About));
            else if (nav == "setting")
                Frame.Navigate(typeof(Settings));
        }



        #region hamburger menu
        //private void menubutton_Click(object sender, RoutedEventArgs e)
        //{
        //    RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        //}
        #endregion


        #region pivot
        private async void  FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Task.Delay(20);
            if (FlipView.SelectedIndex == 0)
            {
                mhome();
                
            }
            if (FlipView.SelectedIndex == 1)
            {
                mtoday();
                
            }
            if (FlipView.SelectedIndex == 2)
            {
                mmonth();
                
               
            }
            if (FlipView.SelectedIndex == 3)
            {
                mpref();
                
            }
        }



        private async void mhome()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 2);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            string message = "Dear";
            DateTime now = DateTime.Now;
            if (now.Hour >= 20 && now.Hour <= 4)
                message = "Good Night";
            if (now.Hour >= 5 && now.Hour <= 9)
                message = "Good morning";
            if (now.Hour >= 13 && now.Hour <= 16)
                message = "Good Afternoon";


            FlipView.SelectedIndex = 0;

            if (isopen == true)
                animate();
            ApplicationData.Current.LocalSettings.Values["SmartieHome"] = +1;
            Task.Delay(20);
            news.Text = (string.Format("{0} {1}", message, ApplicationData.Current.LocalSettings.Values["Username"])).ToUpper();


        }
        private void mtoday()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 2);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            news.Text = "LET'S SEE !";
            FlipView.SelectedIndex = 1;
            if (isopen == true)
                animate();
            ApplicationData.Current.LocalSettings.Values["SmartieToday"] = +1;


        }
        private void mmonth()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 2);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            news.Text = "ENTIRE MONTH";
            FlipView.SelectedIndex = 2;
            if (isopen == true)
                animate();
            ApplicationData.Current.LocalSettings.Values["SmartieMonth"] = +1;

        }
        private void mpref()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 2);
            news.Text = "LOOK OVER IT!";
            FlipView.SelectedIndex = 3;
            if (isopen == true)
                animate();

            ApplicationData.Current.LocalSettings.Values["SmartiePref"] = +1;
        }
        private void bhome_Click(object sender, RoutedEventArgs e)
        {
            mhome();
        }

        private void btoday_Click(object sender, RoutedEventArgs e)
        {
            mtoday();
        }

        private void bmonth_Click(object sender, RoutedEventArgs e)
        {
            mmonth();
        }
        private void bpref_Click(object sender, RoutedEventArgs e)
        {
            mpref();
        }
        #endregion


        #region PanelAnimate
        private void gridMain_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        { 
            // ManipulationDelta hamzaman ba tagheire positione angosht ya mouse emal mishe
            // ma niyaz darim akharin jaei ke manipulate anjam shode begirim
            
            if (e.Position.Y < gridMain.MinHeight) return;
            
            if (lastPostition.Y < 565 && !isopen)
            {
                try
                {
                    detstack.Opacity += e.Delta.Translation.Y / 100;
                    myScaleTransform.Y += e.Delta.Translation.Y;
                    lastPostition.Y += e.Delta.Translation.Y;
                }
                catch { }
            }
            else if(lastPostition.Y >= 185 && isopen)
            {
                try
                {
                    detstack.Opacity += e.Delta.Translation.Y / 100;
                    myScaleTransform.Y += e.Delta.Translation.Y;
                    lastPostition.Y += e.Delta.Translation.Y;
                }
                catch { }
            }
            //
        }
        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            animate();
        }

        private async void animate()
        {
            if (isopen == false)
            {
                try
                {
                    opacitySb1.Begin();
                    myStoryboard.Begin();
                    rotate.Begin();
                    await Task.Delay(500);
                    //detstack.Visibility = Visibility.Visible;
                    isopen = !isopen;
                }
                catch { }

            }
            else
            {
                try
                {
                    opacitySb0.Begin();
                    urStoryboard.Begin();
                    unrotate.Begin();
                    await Task.Delay(300);
                    //detstack.Visibility = Visibility.Collapsed;

                    isopen = !isopen;
                }
                catch (Exception ex) { }
            }
        }
        
        private void gridMain_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.Y > 0 && !isopen) isopen = false;
            if (e.Cumulative.Translation.Y < 0 && isopen) isopen = true;
            if (e.Cumulative.Translation.Y == 0) return;
            animate();
        }


        private void gridMain_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            startpoint = e.Position;
            lastPostition = e.Position;
        }

        #endregion

    }
}
