﻿using Newtonsoft.Json;
using Planel.Classes;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Point startpoint, lastPostition;
        public static MainPage current;
        bool isopen;
        public MainPage()
        {
            InitializeComponent();
            current = this;
            var runtime = (int)ApplicationData.Current.LocalSettings.Values["RunTime"];
            if (runtime <= 4)
            {
                mtoday();
                runtime++;
                ApplicationData.Current.LocalSettings.Values["RunTime"] = runtime;
            }

            coloradjust();
            filsupress();
        }
        async void filsupress()
        {
            // string CountriesFile = @"Assets\Countries.xml";
            // StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            // StorageFile file = await InstallationFolder.GetFileAsync(CountriesFile);
            if (Convert.ToBoolean(Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values["FollowAccent"]) == false))
            {
                if (App.Current.RequestedTheme == ApplicationTheme.Light)
                {
                    Superss.Source = new BitmapImage(new Uri("ms-appx:///Assets/Headings/h3.png"));
                }
                else
                {
                    Superss.Source = new BitmapImage(new Uri("ms-appx:///Assets/Headings/h14.png"));
                }
            }
            else
            {
                Superss.Source = new BitmapImage(new Uri("ms-appx:///Assets/Headings/h14.png"));
            }
        }

        void coloradjust()
        {
            var a = (SolidColorBrush)Application.Current.Resources["AppSuspressBrush"];
            try
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = a.Color;
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = a.Color;
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.InactiveBackgroundColor = a.Color;
                titleBar.ButtonInactiveBackgroundColor = a.Color;
                titleBar.InactiveForegroundColor = Colors.White;
                titleBar.ButtonInactiveForegroundColor = Colors.White;
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonForegroundColor = Colors.White;
                // fuck you asshilism
            }
            catch
            {
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = a.Color;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }

        public void messagesetter(string message) => news.Text = message;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                try
                {
                    Frame.BackStack.Clear();
                }
                catch { }
            }

            var req = await BackgroundExecutionManager.RequestAccessAsync();
            if (req != BackgroundAccessStatus.DeniedByUser && req != BackgroundAccessStatus.DeniedBySystemPolicy)
            {
                var list = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "NotifierTask");
                foreach (var item in list)
                    item.Value.Unregister(false);


                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = "NotifierTask",
                    TaskEntryPoint = "NotifierTask.Notify",
                    CancelOnConditionLoss = false
                };
                taskBuilder.SetTrigger(new TimeTrigger(15, false));

                var list2 = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "MyToastTask");
                foreach (var item in list2)
                    item.Value.Unregister(false);


                var builder = new BackgroundTaskBuilder()
                {
                    Name = "MyToastTask",
                    TaskEntryPoint = "Toaster.Toaster",
                    CancelOnConditionLoss = false
                };

                builder.SetTrigger(new ToastNotificationActionTrigger());
                var registration = builder.Register();
                var myFirstTask = taskBuilder.Register();
            }

            var rootFrame = Window.Current.Content as Frame;

            // string myPages = "";
            // foreach (PageStackEntry page in rootFrame.BackStack)
            // {
            //    myPages += page.SourcePageType.ToString() + "\n";
            // }

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
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.GetFileAsync("avatar.jpg");
            const uint SIZE = 150; //Send your required size
            using (StorageItemThumbnail thumbnail = await sampleFile.GetThumbnailAsync(ThumbnailMode.SingleItem, SIZE))
            {
                if (thumbnail != null)
                {
                    // Prepare thumbnail to display
                    var bitmapImage = new BitmapImage();

                    bitmapImage.SetSource(thumbnail);
                    avatar.ImageSource = bitmapImage;
                }
            }

            var thisday = DateTime.Today;
            todate.Text = thisday.ToString("D");
            counterr(Core.Models.Localdb.counter());

            Classes.worker.tiler();

            base.OnNavigatedTo(e);
            var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
            var isloaded = Settings.isloaded;
            if (args != null && isloaded != true)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
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

                var str = e.Parameter.ToString();

                str = str.Remove(0, 19);
                var toadd = JsonConvert.DeserializeObject<Core.Models.todo>(str);
                var addr = new Core.Models.todo() { notify = toadd.notify, time = toadd.time, title = toadd.title, detail = toadd.detail };

                var msg = new MessageDialog("Do you wanna add this to your todos ?");
                msg.Commands.Add(new UICommand("Yes", async delegate
                {
                    Core.Models.Localdb.Addtodo(addr);
                    Classes.worker.refresher("Add");
                    var noWifiDialog = new ContentDialog()
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
                counter.Text = cnt.ToString() + " " + MultilingualHelpToolkit.GetString("Work", "Text");
            else
                counter.Text = cnt.ToString() + " " + MultilingualHelpToolkit.GetString("worx", "Text");
        }

        public void ntonavigate(string nav)
        {
            if (nav == "about")
                Frame.Navigate(typeof(About));
            else if (nav == "setting")
                Frame.Navigate(typeof(Settings));
        }

        #region hamburger menu
        // private void menubutton_Click(object sender, RoutedEventArgs e)
        // {
        //    RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        // }
        #endregion

        #region pivot
        async void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        async void mhome()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 2);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            var message = MultilingualHelpToolkit.GetString("Greet", "Text");
            var now = DateTime.Now;
            if (now.Hour >= 21 && now.Hour <= 4)
                message = MultilingualHelpToolkit.GetString("night", "Text"); ;
            if (now.Hour >= 5 && now.Hour <= 10)
                message = MultilingualHelpToolkit.GetString("morning", "Text"); ;
            if (now.Hour >= 16 && now.Hour <= 18)
                message = MultilingualHelpToolkit.GetString("eve", "Text"); ;

            FlipView.SelectedIndex = 0;

            if (isopen == true)
                animate();
            ApplicationData.Current.LocalSettings.Values["SmartieHome"] = +1;
            Task.Delay(20);
            news.Text = (string.Format("{0} {1}", message, ApplicationData.Current.LocalSettings.Values["Username"])).ToUpper();
        }

        void mtoday()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 2);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            news.Text = MultilingualHelpToolkit.GetString("letsdo", "Text"); ;
            FlipView.SelectedIndex = 1;
            if (isopen == true)
                animate();
            ApplicationData.Current.LocalSettings.Values["SmartieToday"] = +1;
        }

        void mmonth()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 2);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            news.Text = MultilingualHelpToolkit.GetString("Entire", "Text"); ;
            FlipView.SelectedIndex = 2;
            if (isopen == true)
                animate();
            ApplicationData.Current.LocalSettings.Values["SmartieMonth"] = +1;
        }

        void mpref()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 2);
            news.Text = MultilingualHelpToolkit.GetString("looko", "Text");
            FlipView.SelectedIndex = 3;
            if (isopen == true)
                animate();

            ApplicationData.Current.LocalSettings.Values["SmartiePref"] = +1;
        }

        void bhome_Click(object sender, RoutedEventArgs e) => mhome();

        void btoday_Click(object sender, RoutedEventArgs e) => mtoday();

        void bmonth_Click(object sender, RoutedEventArgs e) => mmonth();

        void bpref_Click(object sender, RoutedEventArgs e) => mpref();
        #endregion

        #region PanelAnimate
        void gridMain_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
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
            else if (lastPostition.Y >= 150 && isopen)
            {
                if (lastPostition.Y < 400)
                {
                    try
                    {
                        detstack.Opacity += e.Delta.Translation.Y / 100;
                        myScaleTransform.Y += e.Delta.Translation.Y;
                        lastPostition.Y += e.Delta.Translation.Y;
                    }
                    catch { }
                }
            }
            //
        }

        void btnShowHide_Click(object sender, RoutedEventArgs e) => animate();

        async void animate()
        {
            if (isopen == false)
            {
                try
                {
                    myStoryboard.Begin();
                    rotate.Begin();
                    opacitySb1.Begin();

                    await Task.Delay(300);
                    // detstack.Visibility = Visibility.Visible;
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
                    // detstack.Visibility = Visibility.Collapsed;

                    isopen = !isopen;
                }
                catch (Exception ex) { }
            }
        }

        void gridMain_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.Y > 0 && !isopen) isopen = false;
            if (e.Cumulative.Translation.Y < 0 && isopen) isopen = true;
            if (e.Cumulative.Translation.Y == 0) return;
            animate();
        }

        void gridMain_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            startpoint = e.Position;
            lastPostition = e.Position;
        }

        #endregion
    }
}