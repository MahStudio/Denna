using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace Denna.Controls
{
    public sealed class DChart : Control
    {
        public DChart()
        {
            this.DefaultStyleKey = typeof(DChart);
        }




        


        public SolidColorBrush SegmentColor1
        {
            get { return (SolidColorBrush)GetValue(SegmentColor1Property); }
            set { SetValue(SegmentColor1Property, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentColor1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColor1Property =
            DependencyProperty.Register(nameof(SegmentColor1), typeof(SolidColorBrush), typeof(DChart), new PropertyMetadata(Colors.Red));



        public SolidColorBrush SegmentColor2
        {
            get { return (SolidColorBrush)GetValue(SegmentColor2Property); }
            set { SetValue(SegmentColor2Property, value); }
        }

        // Using a DependencyProperty as the backing store for SegmentColor2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColor2Property =
            DependencyProperty.Register(nameof(SegmentColor2), typeof(SolidColorBrush), typeof(DChart), new PropertyMetadata(Colors.Black));



        public Double Percentage1
        {
            get { return (Double)GetValue(Percentage1Property); }
            set { SetValue(Percentage1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Percentage1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Percentage1Property =
            DependencyProperty.Register(nameof(Percentage1), typeof(Double), typeof(DChart), new PropertyMetadata(20));


        public Double Percentage2
        {
            get { return (Double)GetValue(Percentage2Property); }
            set { SetValue(Percentage2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Percentage2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Percentage2Property =
            DependencyProperty.Register(nameof(Percentage2), typeof(Double), typeof(DChart), new PropertyMetadata(50));




        public string PercentageText
        {
            get { return (string)GetValue(PercentageTextProperty); }
            set { SetValue(PercentageTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PercentageText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentageTextProperty =
            DependencyProperty.Register(nameof(PercentageText), typeof(string), typeof(DChart), new PropertyMetadata("34%"));




    }
}
