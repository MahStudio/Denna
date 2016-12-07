using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        #region FlipView
        private void flipwel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flipwel.SelectedIndex == 0)
            {
                m1();
            }
            if (flipwel.SelectedIndex == 1)
            {
                m2();
            }
            if (flipwel.SelectedIndex == 2)
            {
                m3();
            }
            if (flipwel.SelectedIndex == 3)
            {
                m4();
            }
            if (flipwel.SelectedIndex == 4)
            {
                m5();
            }
        }
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            m1();
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            m2();
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            m3();
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            m4();
        }
        private void b5_Click(object sender, RoutedEventArgs e)
        {
            m5();
        }
        private void m1()
        {
            b1.Background = new SolidColorBrush( Colors.Gray);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 0;
        }
        private void m2()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.Gray);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 1;
        }
        private void m3()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.Gray);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 2;
        }
        private void m4()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.Gray);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 3;
        }
        private void m5()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.Gray);
            goit.IsEnabled = true;
            flipwel.SelectedIndex = 4;
        }


        #endregion

       
    }
}
