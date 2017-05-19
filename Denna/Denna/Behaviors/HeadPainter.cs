using Microsoft.Xaml.Interactivity;
using System;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Denna.Behaviors
{
    class HeadPainter : Behavior<Page>
    {





        public SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(HeadPainter), new PropertyMetadata(null));


        public SolidColorBrush Brush
        {
            get { return (SolidColorBrush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(SolidColorBrush), typeof(HeadPainter), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.Loaded += Myer;
        }

        private async void Myer(object sender, RoutedEventArgs e)
        {
            SolidColorBrush a = Brush;
            try
            {
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = a.Color;
             
                titleBar.ButtonBackgroundColor = a.Color;
                
                titleBar.InactiveBackgroundColor = a.Color;
                titleBar.ButtonInactiveBackgroundColor = a.Color;
                titleBar.InactiveForegroundColor = Foreground.Color;
                titleBar.ButtonInactiveForegroundColor = Foreground.Color;
                titleBar.ForegroundColor = Foreground.Color;
                titleBar.ButtonForegroundColor = Foreground.Color;
                

                //fuck you asshilism

            }
            catch
            {

            }
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = a.Color;
                    statusBar.ForegroundColor = Foreground.Color;
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= Myer;
        }

    }
}
