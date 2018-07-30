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
            _r1 = R1;
            _r2 = R2;
            _r3 = R3;
            _r1.Click += (s, e) => RightFirstClicked?.Invoke(s, e);
            _r2.Click += (s, e) => RightSecondClicked?.Invoke(s, e);
            _r3.Click += (s, e) => RightThirdClicked?.Invoke(s, e);
        }

        Button _r1;
        Button _r2;
        Button _r3;
        public event EventHandler<RoutedEventArgs> RightFirstClicked;
        public event EventHandler<RoutedEventArgs> RightSecondClicked;
        public event EventHandler<RoutedEventArgs> RightThirdClicked;

        #region props
        public ContentControl TheGrid
        {
            get { return (ContentControl)GetValue(TheGridProperty); }
            set { SetValue(TheGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheGrid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheGridProperty =
            DependencyProperty.Register("TheGrid", typeof(ContentControl), typeof(SwipeListItem), new PropertyMetadata(null));

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



        public string LeftFirstContent
        {
            get { return (string)GetValue(LeftFirstContentProperty); }
            set { SetValue(LeftFirstContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftFirstContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftFirstContentProperty =
            DependencyProperty.Register(nameof(LeftFirstContent), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));


        public SolidColorBrush RightFirstColor
        {
            get { return (SolidColorBrush)GetValue(RightFirstColorProperty); }
            set { SetValue(RightFirstColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightFirstColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightFirstColorProperty =
            DependencyProperty.Register(nameof(RightFirstColor), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public SolidColorBrush RightFirstForeground
        {
            get { return (SolidColorBrush)GetValue(RightFirstForegroundProperty); }
            set { SetValue(RightFirstForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightFirstForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightFirstForegroundProperty =
            DependencyProperty.Register(nameof(RightFirstForeground), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public string RightFirstContent
        {
            get { return (string)GetValue(RightFirstContentProperty); }
            set { SetValue(RightFirstContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightFirstContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightFirstContentProperty =
            DependencyProperty.Register(nameof(RightFirstContent), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));




        public string RightFirstCaption
        {
            get { return (string)GetValue(RightFirstCaptionProperty); }
            set { SetValue(RightFirstCaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightFirstCaption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightFirstCaptionProperty =
            DependencyProperty.Register(nameof(RightFirstCaption), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));



        public SolidColorBrush RightSecondColor
        {
            get { return (SolidColorBrush)GetValue(RightSecondColorProperty); }
            set { SetValue(RightSecondColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSecondColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSecondColorProperty =
            DependencyProperty.Register(nameof(RightSecondColor), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public SolidColorBrush RightSecondForeground
        {
            get { return (SolidColorBrush)GetValue(RightSecondForegroundProperty); }
            set { SetValue(RightSecondForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSecondForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSecondForegroundProperty =
            DependencyProperty.Register(nameof(RightSecondForeground), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public string RightSecondContent
        {
            get { return (string)GetValue(RightSecondContentProperty); }
            set { SetValue(RightSecondContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSecondContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSecondContentProperty =
            DependencyProperty.Register(nameof(RightSecondContent), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));




        public string RightSecondCaption
        {
            get { return (string)GetValue(RightSecondCaptionProperty); }
            set { SetValue(RightSecondCaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightSecondCaption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightSecondCaptionProperty =
            DependencyProperty.Register(nameof(RightSecondCaption), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));



        public SolidColorBrush RightThirdColor
        {
            get { return (SolidColorBrush)GetValue(RightThirdColorProperty); }
            set { SetValue(RightThirdColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightThirdColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightThirdColorProperty =
            DependencyProperty.Register(nameof(RightThirdColor), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public SolidColorBrush RightThirdForeground
        {
            get { return (SolidColorBrush)GetValue(RightThirdForegroundProperty); }
            set { SetValue(RightThirdForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightThirdForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightThirdForegroundProperty =
            DependencyProperty.Register(nameof(RightThirdForeground), typeof(SolidColorBrush), typeof(SwipeListItem), new PropertyMetadata(null));



        public string RightThirdContent
        {
            get { return (string)GetValue(RightThirdContentProperty); }
            set { SetValue(RightThirdContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightThirdContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightThirdContentProperty =
            DependencyProperty.Register(nameof(RightFirstContent), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));



        public string RightThirdCaption
        {
            get { return (string)GetValue(RightThirdCaptionProperty); }
            set { SetValue(RightThirdCaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightThirdCaption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightThirdCaptionProperty =
            DependencyProperty.Register(nameof(RightThirdCaption), typeof(string), typeof(SwipeListItem), new PropertyMetadata(null));


        #endregion


        #region Manipulations
        private void MainGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (myScaleTransform.X >= 60 && e.Delta.Translation.X > 1)
            {
                myScaleTransform.X = 60;
                return;
            }
            if (myScaleTransform.X <= -180 && e.Delta.Translation.X < 1)
            {
                myScaleTransform.X = -180;
                return;
            }
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
