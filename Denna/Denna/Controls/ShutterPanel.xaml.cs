using Microsoft.AppCenter.Analytics;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class ShutterPanel : UserControl
    {
        Point startpoint, lastPostition;
        bool isopen;
        public ShutterPanel()
        {
            InitializeComponent();
        }
        #region PanelAnimate
        void gridMain_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // ManipulationDelta hamzaman ba tagheire positione angosht ya mouse emal mishe
            // ma niyaz darim akharin jaei ke manipulate anjam shode begirim

            if (e.Position.Y < 150) return;

            if (lastPostition.Y < 300 && !isopen)
            {
                try
                {
                    detstack.Opacity += e.Delta.Translation.Y / 100;
                    myScaleTransform.Y += e.Delta.Translation.Y;
                    lastPostition.Y += e.Delta.Translation.Y;
                }
                catch { }
            }
            else if (lastPostition.Y >= 150 && isopen)
            {
                if (lastPostition.Y < 400)
                {
                    try
                    {
                        detstack.Opacity += e.Delta.Translation.Y / 100;
                        myScaleTransform.Y += e.Delta.Translation.Y;
                        lastPostition.Y += e.Delta.Translation.Y;
                    }
                    catch { }
                }
            }
            //
        }

        void btnShowHide_Click(object sender, RoutedEventArgs e) => animate();

        async void animate()
        {
            if (isopen == false)
            {
                try
                {
                    myStoryboard.Begin();
                    rotate.Begin();
                    opacitySb1.Begin();

                    await Task.Delay(300);
                    // detstack.Visibility = Visibility.Visible;
                    isopen = !isopen;
                }
                catch { }
            }
            else
            {
                try
                {
                    opacitySb0.Begin();
                    urStoryboard.Begin();
                    unrotate.Begin();
                    await Task.Delay(300);
                    // detstack.Visibility = Visibility.Collapsed;

                    isopen = !isopen;
                }
                catch (Exception ex) { }
            }

            Analytics.TrackEvent("Swipe panel used");
        }

        void gridMain_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.Y > 0 && !isopen) isopen = false;
            if (e.Cumulative.Translation.Y < 0 && isopen) isopen = true;
            if (e.Cumulative.Translation.Y == 0) return;
            animate();
        }

        void gridMain_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            startpoint = e.Position;
            lastPostition = e.Position;
        }

        #endregion
    }
}