using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Welcome : Page
    {
        public static Welcome current;
        public Welcome()
        {
            InitializeComponent();
            Logger.Navigate(typeof(Sign.In));
            Signerup.Navigate(typeof(Sign.Up));
            current = this;
            KeepChanging();
        }

        private async void KeepChanging()
        {
            while (true)
            {
                var count = flipwel.Items.Count;
                var selected = flipwel.SelectedIndex;
                if (selected != count - 1)
                    flipwel.SelectedIndex++;
                else
                    flipwel.SelectedIndex = 0;

                // Your code here
                await Task.Delay(5500);
            }
        }

        public void opensignup()
        {
            Sign.Visibility = Visibility.Collapsed;
            SignUp.Visibility = Visibility.Visible;
        }

        public void opensignin()
        {
            Sign.Visibility = Visibility.Visible;
            SignUp.Visibility = Visibility.Collapsed;
        }

        #region FlipView
        void flipwel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flipwel.SelectedIndex == 0)
            {
                m1();
            }

            if (flipwel.SelectedIndex == 1)
            {
                m2();
            }

            if (flipwel.SelectedIndex == 2)
            {
                m3();
            }

            if (flipwel.SelectedIndex == 3)
            {
                m4();
            }
        }

        void b1_Click(object sender, RoutedEventArgs e) => m1();

        void b2_Click(object sender, RoutedEventArgs e) => m2();

        void b3_Click(object sender, RoutedEventArgs e) => m3();

        void b4_Click(object sender, RoutedEventArgs e) => m4();

        void m1()
        {
            b1.Opacity = 1;
            b2.Opacity = 0.5;
            b3.Opacity = 0.5;
            b4.Opacity = 0.5;
            flipwel.SelectedIndex = 0;
        }

        void m2()
        {
            b1.Opacity = 0.5;
            b2.Opacity = 1;
            b3.Opacity = 0.5;
            b4.Opacity = 0.5;
            flipwel.SelectedIndex = 1;
        }

        void m3()
        {
            b1.Opacity = 0.5;
            b2.Opacity = 0.5;
            b3.Opacity = 1;
            b4.Opacity = 0.5;
            flipwel.SelectedIndex = 2;
        }

        void m4()
        {
            b1.Opacity = 0.5;
            b2.Opacity = 0.5;
            b3.Opacity = 0.5;
            b4.Opacity = 1;
            flipwel.SelectedIndex = 3;
        }

        #endregion
    }
}