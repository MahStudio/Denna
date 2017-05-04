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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Graphs : Page
    {
        private Random _random = new Random();
        public Graphs()
        {
            this.InitializeComponent();
            List<NameValueItem> items = new List<NameValueItem>();
            items.Add(new NameValueItem { Name = "Test1", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test2", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test3", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test4", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test5", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test6", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test7", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test8", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test9", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test10", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test11", Value = _random.Next(10, 100) });
            items.Add(new NameValueItem { Name = "Test12", Value = _random.Next(10, 100) });
            ((LineSeries)this.LineChart2.Series[0]).ItemsSource = items;
            var series = (LineSeries)this.LineChart2.Series[0];
            series.ItemsSource = items;
            ((LineSeries)this.LineChart2.Series[0]).DependentRangeAxis = new LinearAxis

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
            




        }
    }
    public class NameValueItem
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
