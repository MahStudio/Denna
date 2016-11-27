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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double lastPostition;

        public MainPage()
        {
            this.InitializeComponent();
           

        }
        private void menubutton_Click(object sender, RoutedEventArgs e)
        {
            RootSplitView.IsPaneOpen = !RootSplitView.IsPaneOpen;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            fhome.Navigate(typeof(fhome));
            ftoday.Navigate(typeof(ftoday));
            fmonth.Navigate(typeof(fmonth));

        }
        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        #region PanelAnimate

        private void btnShowHide_Click(object sender, RoutedEventArgs e)
        {
            // age tage reshte ei controle button barabrare icon e robe bala bod,
            // pas ma niyaz darim ke control ro be hesabi hide konim ,
            // ama dar asl darim height ro mikonim 100
            if ((string)btnShowHide. Content == "")
                Animate(gridMain, true);
            else
                Animate(gridMain);
        }
        /// <summary>
        /// Hide ya Show kardane control
        /// </summary>
        /// <param name="uiElement">elementi ke mikhaim animate konim</param>
        /// <param name="hide">penhan kardan ya namyeshe control</param>
        void Animate(UIElement uiElement, bool hide = false)
        {
            Storyboard s = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromMilliseconds(250));
            da.EnableDependentAnimation = true;
            // age hide barabare false bod, barabare actual height gharar midim
            if (!hide)
                da.To = ActualHeight/2;
            else
                // age ham na, barabare 100 kon
                da.To = 180;
            s.Children.Add(da);
            Storyboard.SetTarget(da, uiElement);
            Storyboard.SetTargetProperty(da, "(FrameworkElement.Height)");
            s.Begin();
        }

        private void gridMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // age height e jadide control barabare 100 bod,
            // miaim tag va icone btnShowHide ro barabare icone robe paein gharar midim
            // man az barname Character Map estefade kardam
            if (e.NewSize.Height == 180)
            {
                btnShowHide.Content = "";
                
            }
            // age ham na, bayad icone robe bala ro neshon bedim
            else
            {
                btnShowHide.Content = "";
                
            }
        }

        private void gridMain_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // ManipulationDelta hamzaman ba tagheire positione angosht ya mouse emal mishe
            // ma niyaz darim akharin jaei ke manipulate anjam shode begirim
            lastPostition = e.Position.Y;
        }

        private void gridMain_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            // age akharin jaei ke manipulate shode kochik bozorg tar az positione fe'lie
            // hide ro anjam midim
            if (e.Position.Y < lastPostition)
                Animate(gridMain, true);
            else
                Animate(gridMain);
        }

        #endregion
    }
}
