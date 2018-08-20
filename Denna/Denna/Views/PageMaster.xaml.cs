using Denna.Views.SubMaster;
using System;
using PubSub;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Denna.Classes;
using Core.Service.Notifications;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageMaster : Page
    {
        public static PageMaster current;
        BackgroundService _bgService;
        public PageMaster()
        {
            InitializeComponent();
            _bgService = new BackgroundService();
            current = this;
        }
        public void NavigateToUnitTests() => Frame.Navigate(typeof(UnitTests));

        public void NavigateToSettings() => Frame.Navigate(typeof(Settings));

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            #region BackgroundTasks
            //BackgroundHelper.RegisterBackgroundServices();
            #endregion
            #region Back handle
            if (Frame.CanGoBack)
            {
                try
                {
                    Frame.BackStack.Clear();
                }
                catch { }
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            #endregion
            #region navigations
            TimeLine.Navigate(typeof(TimeLine));
            Chats.Navigate(typeof(Graphs));
            #endregion
            //_bgService.UpdateTiles();
        }

        #region Pivot
        void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // await Task.Delay(20);
            if (Pivot.SelectedIndex == 0)
                mtoday();
            if (Pivot.SelectedIndex == 1)
                mpref();
        }

        void mtoday()
        {
            btoday.BorderThickness = new Thickness(0, 0, 0, 2);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            Pivot.SelectedIndex = 0;
            if (TimeLine.Content.GetType() != typeof(SubMaster.Add.Task))
                this.Publish(new Classes.Header("Timeline"));
            else
                this.Publish(new Classes.Header("Add"));
        }

        void mpref()
        {
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 2);
            this.Publish(new Classes.Header("Performance"));
            Pivot.SelectedIndex = 1;
        }

        void btoday_Click(object sender, RoutedEventArgs e) => mtoday();

        void bpref_Click(object sender, RoutedEventArgs e) => mpref();
        #endregion
    }
}