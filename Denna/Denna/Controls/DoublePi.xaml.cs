using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class DoublePi : UserControl
    {
        public DoublePi()
        {
            InitializeComponent();
        }

        public SolidColorBrush SegmentColor1
        {
            get { return (SolidColorBrush)GetValue(SegmentColor1Property); }
            set { SetValue(SegmentColor1Property, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentColor1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColor1Property =
            DependencyProperty.Register(nameof(SegmentColor1), typeof(SolidColorBrush), typeof(DoublePi), new PropertyMetadata(null));

        public SolidColorBrush SegmentColor2
        {
            get { return (SolidColorBrush)GetValue(SegmentColor2Property); }
            set { SetValue(SegmentColor2Property, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentColor2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColor2Property =
            DependencyProperty.Register(nameof(SegmentColor2), typeof(SolidColorBrush), typeof(DoublePi), new PropertyMetadata(null));

        public double Percentage1
        {
            get { return (double)GetValue(Percentage1Property); }
            set { SetValue(Percentage1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Percentage1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Percentage1Property =
            DependencyProperty.Register(nameof(Percentage1), typeof(double), typeof(DoublePi), new PropertyMetadata(null));

        public double Percentage2
        {
            get { return (double)GetValue(Percentage2Property); }
            set { SetValue(Percentage2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Percentage2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Percentage2Property =
            DependencyProperty.Register(nameof(Percentage2), typeof(double), typeof(DoublePi), new PropertyMetadata(null));
    }
}