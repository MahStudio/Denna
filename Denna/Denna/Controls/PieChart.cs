using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Denna.Controls
{
    public class PieChart : UserControl
    {
        double angle = 120;
        ArcSegment arcSegment = new ArcSegment();
        ArcSegment arcSegment360 = new ArcSegment();
        PathFigure pathFigure = new PathFigure();
        PathFigure pathFigure360 = new PathFigure();
        Path pathRoot = new Path();
        Path pathRoot360 = new Path();

        public int Radius
        {
            get
            {
                return (int)GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }

        public Brush SegmentColor
        {
            get
            {
                return (Brush)GetValue(SegmentColorProperty);
            }
            set
            {
                SetValue(SegmentColorProperty, value);
            }
        }

        public Brush Segment360Color
        {
            get
            {
                return (Brush)GetValue(Segment360ColorProperty);
            }
            set
            {
                SetValue(Segment360ColorProperty, value);
            }
        }

        public Brush BackgroundColor
        {
            get
            {
                return (Brush)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        public int StrokeThickness
        {
            get
            {
                return (int)GetValue(StrokeThicknessProperty);
            }
            set
            {
                SetValue(StrokeThicknessProperty, value);
            }
        }

        public double Percentage
        {
            get
            {
                return (double)GetValue(PercentageProperty);
            }
            set
            {
                SetValue(PercentageProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(PieChart), new PropertyMetadata(65d, OnPropertyChanged));

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(int), typeof(PieChart), new PropertyMetadata(10, OnPropertyChanged));

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColorProperty =
            DependencyProperty.Register("SegmentColor", typeof(Brush), typeof(PieChart), new PropertyMetadata(new SolidColorBrush(Colors.Green), OnPropertyChanged));

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(PieChart), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), OnPropertyChanged));

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Segment360ColorProperty =
            DependencyProperty.Register("Segment360Color", typeof(Brush), typeof(PieChart), new PropertyMetadata(new SolidColorBrush(Colors.LightGray), OnPropertyChanged));

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(PieChart), new PropertyMetadata(25, OnPropertyChanged));

        public PieChart()
        {
            InitializePieChart();

            // _angle = Percentage * 360 / 100;
        }

        static void OnPercentageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var pieChart = sender as PieChart;

            pieChart.angle = (pieChart.Percentage * 360) / 100;

            pieChart.InitializePieChart();

            pieChart.RenderArc();
        }

        static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var pieChart = sender as PieChart;

            pieChart.InitializePieChart();

            pieChart.RenderArc();
        }

        public void RenderArc()
        {
            angle = Percentage * 360 / 100;

            var startPoint = new Point(Radius, 0);
            var endPoint = ComputeCartesianCoordinate(angle, Radius);
            endPoint.X += Radius;
            endPoint.Y += Radius;

            pathRoot.Width = Radius * 2 + StrokeThickness;
            pathRoot.Height = Radius * 2 + StrokeThickness;
            pathRoot.Margin = new Thickness(StrokeThickness, StrokeThickness, 0, 0);

            var largeArc = angle > 180.0;

            var outerArcSize = new Size(Radius, Radius);

            pathFigure.StartPoint = startPoint;

            if (startPoint.X == Math.Round(endPoint.X) && startPoint.Y == Math.Round(endPoint.Y))
            {
                endPoint.X -= 0.01;
            }

            arcSegment.Point = endPoint;
            arcSegment.Size = outerArcSize;
            arcSegment.IsLargeArc = largeArc;

            // Draw the 360 arc/circle

            var endPoint2 = ComputeCartesianCoordinate(360, Radius);
            endPoint2.X += Radius;
            endPoint2.Y += Radius;

            pathRoot360.Width = Radius * 2 + StrokeThickness;
            pathRoot360.Height = Radius * 2 + StrokeThickness;
            pathRoot360.Margin = new Thickness(StrokeThickness, StrokeThickness, 0, 0);

            pathFigure360.StartPoint = startPoint;

            if (startPoint.X == Math.Round(endPoint2.X) && startPoint.Y == Math.Round(endPoint2.Y))
            {
                endPoint2.X -= 0.01;
            }

            arcSegment360.Point = endPoint2;
            arcSegment360.Size = outerArcSize;
            arcSegment360.IsLargeArc = true;
        }

        Point ComputeCartesianCoordinate(double angle, double radius)
        {
            // convert to radians
            var angleRad = (Math.PI / 180.0) * (angle - 90);

            var x = radius * Math.Cos(angleRad);
            var y = radius * Math.Sin(angleRad);

            return new Point(x, y);
        }

        public void InitializePieChart()
        {
            // draw the full circle/arc
            arcSegment360 = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise
            };

            pathFigure360 = new PathFigure
            {
                Segments = new PathSegmentCollection
                {
                    arcSegment360
                }
            };

            pathRoot360 = new Path
            {
                Stroke = Segment360Color,
                StrokeThickness = StrokeThickness,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Data = new PathGeometry
                {
                    Figures = new PathFigureCollection
                    {
                        pathFigure360
                    }
                }
            };

            // draw a circle with the given angle

            arcSegment = new ArcSegment
            {
                SweepDirection = SweepDirection.Clockwise
            };

            pathFigure = new PathFigure
            {
                Segments = new PathSegmentCollection
                {
                    arcSegment
                }
            };

            pathRoot = new Path
            {
                Stroke = SegmentColor,
                StrokeThickness = StrokeThickness,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Data = new PathGeometry
                {
                    Figures = new PathFigureCollection
                    {
                        pathFigure
                    }
                }
            };

            Content = new Grid
            {
                Background = BackgroundColor,
                Children =
                {
                    pathRoot360,
                    pathRoot,
                }
            };
        }
    }
}