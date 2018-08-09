using Denna.Classes;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(GraphControl), new PropertyMetadata(null));

        public int Done
        {
            get { return (int)GetValue(DoneProperty); }
            set { SetValue(DoneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Done.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoneProperty =
            DependencyProperty.Register("Done", typeof(int), typeof(GraphControl), new PropertyMetadata(0));

        public int Pending
        {
            get { return (int)GetValue(PendingProperty); }
            set { SetValue(PendingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pending.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PendingProperty =
            DependencyProperty.Register("Pending", typeof(int), typeof(GraphControl), new PropertyMetadata(0));

        public ObservableCollection<NameValueItem> Items
        {
            get { return (ObservableCollection<NameValueItem>)GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
                ((LineSeries)LineChart2.Series[0]).ItemsSource = Items;
                var series = (LineSeries)LineChart2.Series[0];
                series.ItemsSource = Items;
                ((LineSeries)LineChart2.Series[0]).DependentRangeAxis = new LinearAxis

                {
                    Minimum = 0,
                    Maximum = 100,
                    Orientation = AxisOrientation.Y,
                    Interval = 20,
                    ShowGridLines = false,
                    Width = 0
                };
                series.Foreground = new SolidColorBrush(Colors.Red);
                series.IndependentAxis =
                                new CategoryAxis
                                {
                                    Orientation = AxisOrientation.X,
                                    Height = 0
                                };
            }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<NameValueItem>), typeof(GraphControl), new PropertyMetadata(null));
    }
}