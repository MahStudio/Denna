using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class IconItem : UserControl
    {
        public IconItem()
        {
            InitializeComponent();
        }

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register("Glyph", typeof(string), typeof(IconItem), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IconItem), new PropertyMetadata(null));

        public SolidColorBrush Brush
        {
            get { return (SolidColorBrush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(SolidColorBrush), typeof(IconItem), new PropertyMetadata(null));

        public int FontSizeGlyph
        {
            get { return (int)GetValue(FontSizeGlyphProperty); }
            set { SetValue(FontSizeGlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSizeGlyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeGlyphProperty =
            DependencyProperty.Register("FontSizeGlyph", typeof(int), typeof(IconItem), new PropertyMetadata(25));
    }
}