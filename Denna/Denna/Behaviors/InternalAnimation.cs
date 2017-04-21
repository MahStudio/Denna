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
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += Myer ;
        }

        private void Myer(object sender, RoutedEventArgs e)
        {
            var x = AssociatedObject.Percentage;
            Storyboard myboard = new Storyboard();
            DoubleAnimation Addvalues = new DoubleAnimation();
            Addvalues.To = x;
            Addvalues.From = 0;
            Addvalues.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
            Storyboard.SetTarget(Addvalues, AssociatedObject);
            Storyboard.SetTargetProperty(Addvalues, "Percentage");
            myboard.Children.Add(Addvalues);
            myboard.Begin();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= Myer;
        }
        
    }
}
