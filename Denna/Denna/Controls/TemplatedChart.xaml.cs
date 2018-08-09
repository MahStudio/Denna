using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class TemplatedChart : UserControl
    {
        public TemplatedChart()
        {
            InitializeComponent();
        }

        public SolidColorBrush SegmentColor
        {
            get { return (SolidColorBrush)GetValue(SegmentColorProperty); }
            set { SetValue(SegmentColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColorProperty =
            DependencyProperty.Register("SegmentColor", typeof(SolidColorBrush), typeof(TemplatedChart), new PropertyMetadata(null));

        public SolidColorBrush GlyphBrush
        {
            get { return (SolidColorBrush)GetValue(GlyphBrushProperty); }
            set { SetValue(GlyphBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GlyphBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlyphBrushProperty =
            DependencyProperty.Register("GlyphBrush", typeof(SolidColorBrush), typeof(TemplatedChart), new PropertyMetadata(null));

        public double Percent1
        {
            get { return (double)GetValue(Percent1Property); }
            set { SetValue(Percent1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Percent1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Percent1Property =
            DependencyProperty.Register("Percent1", typeof(double), typeof(TemplatedChart), new PropertyMetadata(null));

        public double Percent2
        {
            get { return (double)GetValue(Percent2Property); }
            set { SetValue(Percent2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Percent2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Percent2Property =
            DependencyProperty.Register("Percent2", typeof(double), typeof(TemplatedChart), new PropertyMetadata(null));

        public int DoneTasks
        {
            get { return (int)GetValue(DoneTasksProperty); }
            set { SetValue(DoneTasksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DoneTasks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoneTasksProperty =
            DependencyProperty.Register("DoneTasks", typeof(int), typeof(TemplatedChart), new PropertyMetadata(null));

        public int PostponedTasks
        {
            get { return (int)GetValue(PostponedTasksProperty); }
            set { SetValue(PostponedTasksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PostponedTasks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PostponedTasksProperty =
            DependencyProperty.Register("PostponedTasks", typeof(int), typeof(TemplatedChart), new PropertyMetadata(null));

        public int PendingTasks
        {
            get { return (int)GetValue(PendingTasksProperty); }
            set { SetValue(PendingTasksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PendingTasks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PendingTasksProperty =
            DependencyProperty.Register("PendingTasks", typeof(int), typeof(TemplatedChart), new PropertyMetadata(null));
    }
}