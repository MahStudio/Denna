using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MainPage : Page
    {
        int a = 0;

        public MainPage()
        {
            this.InitializeComponent();
            
        }
        private void menubutton_Click(object sender, RoutedEventArgs e)
        {
            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        }

        private void Changebut_Click(object sender, RoutedEventArgs e)
        {
            if (a == 1)
            {
                Panel.Height = 180;
                Changebut.Content = "";
                a = 0;

            }
            else
            {
                Panel.Height = 280;
                Changebut.Content = "";
                a = 1;
            }
            
            
            
            
        }
    }
}
