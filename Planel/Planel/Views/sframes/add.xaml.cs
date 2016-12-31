using System;
using System.Collections.Generic;
using System.Globalization;
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
            
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //ignore
            Frame.Navigate(typeof(ftoday));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            //add to database
            DateTime todate = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes,timepic.Time.Seconds);
          
            
                
            Models.Localdb.Addtodo(title.Text,describe.Text,todate);
            Classes.worker.refresher();
           
        }
    }
}
