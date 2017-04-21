using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class SwipeListItem : UserControl
    {
        Point startpoint;
        Point lastPostition;
        public SwipeListItem()
        {
            this.InitializeComponent();
        }



        public SolidColorBrush LeftFirstColor
        {
            get { return (SolidColorBrush)GetValue(LeftFirstColorProperty); }
            set { SetValue(LeftFirstColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftFirstColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftFirstColorProperty =
            DependencyProperty.Register(nameof(LeftFirstColor), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public SolidColorBrush LeftFirstForeground
        {
            get { return (SolidColorBrush)GetValue(LeftFirstForegroundProperty); }
            set { SetValue(LeftFirstForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftFirstForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftFirstForegroundProperty =
            DependencyProperty.Register(nameof(LeftFirstForeground), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));





        #region Manipulations
        private void MainGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (myScaleTransform.X >= 60 && e.Delta.Translation.X >1) return;
            if (myScaleTransform.X <= -180 && e.Delta.Translation.X < 1) return;
            myScaleTransform.X += e.Delta.Translation.X;
            lastPostition.X += e.Delta.Translation.X;
           
        }

        private void MainGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (myScaleTransform.X <= -170 ) return;
            urStoryboard.Begin();

        }

        private void MainGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            startpoint = e.Position;
            lastPostition = e.Position;
        }
    }
    #endregion
}
