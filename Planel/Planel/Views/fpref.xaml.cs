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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class fpref : Page
    {
        public fpref()
        {
            this.InitializeComponent();
            var items1 = new List<NameValueItem>();
            var items2 = new List<NameValueItem>();
            var items3 = new List<NameValueItem>();
            items1.Add(new NameValueItem { Name = "Sat" , Value = 50 });
            items1.Add(new NameValueItem { Name = "Sun", Value = 50 });
            items1.Add(new NameValueItem { Name = "Mon", Value = 50 });
            items1.Add(new NameValueItem { Name = "Tue", Value = 60 });
            items1.Add(new NameValueItem { Name = "Wed", Value = 90 });
            items1.Add(new NameValueItem { Name = "Thu", Value = 10 });
            items1.Add(new NameValueItem { Name = "Fri", Value = 30 });


            var series = (AreaSeries)this.AreaChartWithNoLabels.Series[0];
            series.ItemsSource = items1;


            series.DependentRangeAxis =
                new LinearAxis
                {
                    Minimum = 0,
                    Maximum = 100,
                    Orientation = AxisOrientation.Y,
                    Interval = 20,
                    ShowGridLines = false,
                    Width = 0
                };
            series.IndependentAxis =
                new CategoryAxis
                {
                    Orientation = AxisOrientation.X,
                    Height = 0
                };
            var series2 = (AreaSeries)this.AreaChartWithNoLabels2.Series[0];
            series2.ItemsSource = items1;


            series2.DependentRangeAxis =
                new LinearAxis
                {
                    Minimum = 0,
                    Maximum = 100,
                    Orientation = AxisOrientation.Y,
                    Interval = 20,
                    ShowGridLines = false,
                    Width = 0
                };
            series2.IndependentAxis =
                new CategoryAxis
                {
                    Orientation = AxisOrientation.X,
                    Height = 0
                };

        }
                

        }
        



    }
    public class NameValueItem
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

