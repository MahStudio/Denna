using Denna.Views.SubSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private void ArtistsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clk = e.ClickedItem as Classes.ItemHolder;
            switch (clk.ID)
            {
                case 1 :
                    {
                        Frame.Navigate(typeof(Account));
                        break;
                    }
                case 2:
                    {
                        Frame.Navigate(typeof(Privacy));
                        break;
                    }
                case 3:
                    {
                        Frame.Navigate(typeof(Notifications));
                        break;
                    }
                case 4:
                    {
                        Frame.Navigate(typeof(QuickActions));
                        break;
                    }
                case 5:
                    {
                        Frame.Navigate(typeof(Personalization));
                        break;
                    }
                case 6:
                    {
                        Frame.Navigate(typeof(Language));
                        break;
                    }
                case 7:
                    {
                        Frame.Navigate(typeof(Support));
                        break;
                    }
                case 8:
                    {
                        Frame.Navigate(typeof(About));
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
