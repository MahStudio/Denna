
using Windows.UI.Xaml.Controls;
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
            var items1 = Core.Models.Localdb.Wgraph();
            var items2 = Core.Models.Localdb.Mgraph();         


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
            series2.ItemsSource = items2;


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
    

