using Denna.Views.SubMaster;
using PubSub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            this.InitializeComponent();
            current = this;
        }
        public void NavigateToUnitTests()
        {
            Frame.Navigate(typeof(UnitTests));
        }
        public void NavigateToSettings()
        {
            Frame.Navigate(typeof(Settings));
        }
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
            Home.Navigate(typeof(Home));
            TimeLine.Navigate(typeof(TimeLine));
            CalView.Navigate(typeof(Calendar));
            Chats.Navigate(typeof(SubMaster.Graphs));
#endregion

        }

#region Pivot
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // await Task.Delay(20);
            if (Pivot.SelectedIndex == 0)
            {
                mhome();

            }
            if (Pivot.SelectedIndex == 1)
            {
                mtoday();

            }
            if (Pivot.SelectedIndex == 2)
            {
                mmonth();


            }
            if (Pivot.SelectedIndex == 3)
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
            this.Publish(new Classes.Header("Dear Insider"));


            Pivot.SelectedIndex = 0;


        }
        private void mtoday()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 2);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            Pivot.SelectedIndex = 1;
            this.Publish(new Classes.Header("Timeline"));


        }
        private void mmonth()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 2);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            Pivot.SelectedIndex = 2;
            this.Publish(new Classes.Header("Entire month"));
        }
        private void mpref()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 2);
            this.Publish(new Classes.Header("Performance"));
            Pivot.SelectedIndex = 3;
            
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
    }
}
