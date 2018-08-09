using Denna.Views.SubMaster;
using PubSub;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageMaster : Page
    {
        public static PageMaster current;
        public PageMaster()
        {
            InitializeComponent();
            current = this;
        }
        public void NavigateToUnitTests() => Frame.Navigate(typeof(UnitTests));

        public void NavigateToSettings() => Frame.Navigate(typeof(Settings));

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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
            this.Publish(new Classes.Header("Timeline"));
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