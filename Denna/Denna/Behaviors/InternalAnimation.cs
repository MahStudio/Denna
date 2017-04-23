using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Denna.Behaviors
{
    class InternalAnimation : Behavior<Controls.PieChart>
    {
        //public int IntendedValue
        //{
        //    get { return (int)GetValue(IntendedValueProperty); }
        //    set { SetValue(IntendedValueProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IntendedValue.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IntendedValueProperty =
        //    DependencyProperty.Register(nameof(IntendedValue), typeof(int),
        //        typeof(InternalAnimation), new PropertyMetadata(0, IntendedValuePropertyChangedCallback));


        public Double IntendedValue
        {
            get { return (Double)GetValue(IntendedValueProperty); }
            set { SetValue(IntendedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IntendedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntendedValueProperty =
            DependencyProperty.Register("IntendedValue", typeof(Double), typeof(InternalAnimation), new PropertyMetadata(null, IntendedValuePropertyChangedCallback));



        private static void IntendedValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var IncrementBehavior = d as InternalAnimation;
            if (IncrementBehavior == null) return;
            var value = (Double)e.NewValue;
            if (value < 0)
                IncrementBehavior.IntendedValue = 0;
            else if (value > 100)
                IncrementBehavior.IntendedValue = 100;
            else
                IncrementBehavior.IntendedValue = value;
        }

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += Myer;
        }

        private void Myer(object sender, RoutedEventArgs e)
        {
            Storyboard myboard = new Storyboard();
            DoubleAnimation AddValueDoubleAnimation = new DoubleAnimation();

            AddValueDoubleAnimation.To = IntendedValue;
            AddValueDoubleAnimation.From = 0;
            AddValueDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(700));
            
            AddValueDoubleAnimation.EnableDependentAnimation = true;
            Storyboard.SetTarget(AddValueDoubleAnimation, AssociatedObject);
            Storyboard.SetTargetProperty(AddValueDoubleAnimation, "Percentage");
            myboard.Children.Add(AddValueDoubleAnimation);
            myboard.Begin();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= Myer;
        }

    }
}
