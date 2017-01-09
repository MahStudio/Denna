using System;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
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
        double lastPostition;
        public static MainPage current;
        public MainPage()
        {
            this.InitializeComponent();
            current = this;



        }

        

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                var req = await BackgroundExecutionManager.RequestAccessAsync();
                if (req == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity || req == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    var list = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "NotifierTask");
                    foreach (var item in list)
                    {
                        item.Value.Unregister(false);
                    }
                    BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder { Name = "NotifierTask", TaskEntryPoint = "NotifierTask.Notify" };
                    taskBuilder.SetTrigger(new TimeTrigger(15, false));

                    BackgroundTaskRegistration myFirstTask = taskBuilder.Register();
                }

            }
            catch (Exception)
            {

            }
            Frame rootFrame = Window.Current.Content as Frame;

            string myPages = "";
            foreach (PageStackEntry page in rootFrame.BackStack)
            {
                myPages += page.SourcePageType.ToString() + "\n";
            }
            

            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
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
            counterr(Models.Localdb.counter());
            string message = "Dear";
            DateTime now = DateTime.Now;
            if (now.Hour >= 20 && now.Hour <= 4)
                message = "Good Night";
            if (now.Hour >= 5 && now.Hour <= 9)
                message = "Good morning";
            if (now.Hour >= 13 && now.Hour <= 16)
                message = "Good Afternoon";

            news.Text = (string.Format("{0} {1}", message, ApplicationData.Current.LocalSettings.Values["Username"])).ToUpper();
            Classes.worker.tiler();

        }

        public void counterr(int cnt)
        {



            if (cnt == 1 || cnt ==0 )
                counter.Text = string.Format("You have {0} work to do", cnt.ToString());
            else
                counter.Text = string.Format("You have {0} works to do", cnt.ToString());

        }
        public void ntonavigate(string nav)
        {
            if (nav=="about")
            Frame.Navigate(typeof(About));
            else if (nav=="setting")
                Frame.Navigate(typeof(Settings));
        }



        #region hamburger menu
        //private void menubutton_Click(object sender, RoutedEventArgs e)
        //{
        //    RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        //}
        #endregion

        
        #region pivot
        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isHardwareButtonsAPIPresent =
                Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            if(isHardwareButtonsAPIPresent)
            {
                HardwareButtons.BackPressed -= HardwareButtons0_BackPressed;
                HardwareButtons.BackPressed -= HardwareButtons1_BackPressed;
                HardwareButtons.BackPressed -= HardwareButtons2_BackPressed;
                HardwareButtons.BackPressed -= HardwareButtons3_BackPressed;
            }
            if (FlipView.SelectedIndex==0)
            {
                mhome();
                if(isHardwareButtonsAPIPresent)
                {
                    HardwareButtons.BackPressed += HardwareButtons0_BackPressed;
                }
            }
            if (FlipView.SelectedIndex == 1)
            {
                mtoday();
                if (isHardwareButtonsAPIPresent)
                {
                    HardwareButtons.BackPressed += HardwareButtons1_BackPressed;
                }
            }
            if (FlipView.SelectedIndex == 2)
            {
                mmonth();
                if (isHardwareButtonsAPIPresent)
                {
                    HardwareButtons.BackPressed += HardwareButtons2_BackPressed;
                }
            }
            if (FlipView.SelectedIndex == 3)
            {
                mpref();
                if (isHardwareButtonsAPIPresent)
                {
                    HardwareButtons.BackPressed += HardwareButtons3_BackPressed;
                }
            }
        }
        
        private void HardwareButtons0_BackPressed(object sender, BackPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HardwareButtons1_BackPressed(object sender, BackPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HardwareButtons2_BackPressed(object sender, BackPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HardwareButtons3_BackPressed(object sender, BackPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void mhome()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 2);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            
            FlipView.SelectedIndex = 0;

            Animate(gridMain, true);

        }
        private void mtoday()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 2);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            news.Text = "LET'S DO TODAY'S!";
            FlipView.SelectedIndex = 1;
            Animate(gridMain, true);


        }
        private void mmonth()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 2);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            news.Text = "ENTIRE MONTH";
            FlipView.SelectedIndex = 2;
            Animate(gridMain, true); 

        }
        private void mpref()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 2);
            news.Text = "LOOK OVER IT!";
            FlipView.SelectedIndex = 3;
            Animate(gridMain, true);
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
            lastPostition = e.Position.Y;
            if (e.Position.Y < gridMain.MinHeight) return;
            try
            {
                Storyboard s = new Storyboard();
                DoubleAnimation da = new DoubleAnimation();
                da.Duration = new Duration(TimeSpan.FromMilliseconds(250));
                da.EnableDependentAnimation = true;
                // age hide barabare false bod, barabare actual height gharar midim
                // age ham na, barabare 100 kon
                da.To = e.Position.Y;
                s.Children.Add(da);
                Storyboard.SetTarget(da, gridMain);
                Storyboard.SetTargetProperty(da, "(FrameworkElement.Height)");
                s.Begin();
            }
            catch { }
        }
        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            // age tage reshte ei controle button barabrare icon e robe bala bod,
            // pas ma niyaz darim ke control ro be hesabi hide konim ,
            // ama dar asl darim height ro mikonim 100
            if ((string)btnShowHide. Content == "")
                Animate(gridMain, true);
            else
                Animate(gridMain);
        }
        /// <summary>
        /// Hide ya Show kardane control
        /// </summary>
        /// <param name="uiElement">elementi ke mikhaim animate konim</param>
        /// <param name="hide">penhan kardan ya namyeshe control</param>
        void Animate(UIElement uiElement, bool hide = false)
        {
            Storyboard s = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromMilliseconds(250));
            da.EnableDependentAnimation = true;
            // age hide barabare false bod, barabare actual height gharar midim
            if (!hide)
                da.To = 400;
            else
                // age ham na, barabare 100 kon
                da.To = 180;
            s.Children.Add(da);
            Storyboard.SetTarget(da, uiElement);
            Storyboard.SetTargetProperty(da, "(FrameworkElement.Height)");
            s.Begin();
        }

        private void gridMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // age height e jadide control barabare 100 bod,
            // miaim tag va icone btnShowHide ro barabare icone robe paein gharar midim
            // man az barname Character Map estefade kardam
            if (e.NewSize.Height == 180)
            {
                btnShowHide.Content = "";
                
            }
            // age ham na, bayad icone robe bala ro neshon bedim
            else
            {
                btnShowHide.Content = "";
                
            }
        }

       

        private void gridMain_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            // age akharin jaei ke manipulate shode kochik bozorg tar az positione fe'lie
            // hide ro anjam midim
            if (e.Position.Y < lastPostition)
                Animate(gridMain, true);
            else
                Animate(gridMain);
        }


        #endregion

        
    }
}
