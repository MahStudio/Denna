using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            this.InitializeComponent();
            Models.Localdb.CreateDatabase();
        }

       
        #region FlipView
        private void flipwel_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            if (flipwel.SelectedIndex == 4)
            {
                m5();
            }
        }
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            m1();
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            m2();
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            m3();
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            m4();
        }
        private void b5_Click(object sender, RoutedEventArgs e)
        {
            m5();
        }
        private void m1()
        {
            b1.Background = new SolidColorBrush( Colors.Gray);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 0;
        }
        private void m2()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.Gray);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 1;
        }
        private void m3()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.Gray);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 2;
        }
        private void m4()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.Gray);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 3;
        }
        private void m5()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.Gray);
            goit.IsEnabled = true;
            flipwel.SelectedIndex = 4;
        }


        #endregion


        
        private string name;
        private string filename;

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

               
                // Application now has read/write access to the picked file
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                
                try
                {
                    Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
          Windows.Storage.StorageFile sampleFile =
              await storageFolder.GetFileAsync("avatar.jpg");
                    sampleFile.DeleteAsync(StorageDeleteOption.PermanentDelete); 
                    
                }
                catch { }
               
                StorageFile copiedFile = await file.CopyAsync(localFolder,"avatar.jpg");
                filename = "avatar.jpg"; 
                StorageFolder storageFolder2 = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile2 = await storageFolder2.GetFileAsync("avatar.jpg");

                avatar.ImageSource = new BitmapImage(new Uri(sampleFile2.Path));
                
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["SmartieToday"] = 3;
            ApplicationData.Current.LocalSettings.Values["SmartieHome"] = 0;
            ApplicationData.Current.LocalSettings.Values["SmartiePref"] = 0;
            ApplicationData.Current.LocalSettings.Values["SmartieMonth"] = 0;
            if (namebox.Text != "")
            name = namebox.Text;
            else
            {
                var messageDialog = new MessageDialog("Fill your name and picture and press agian :)");
                messageDialog.ShowAsync();
            }
            if (name != null && filename != null)
            {
                Models.Localdb.Iuser( name);
                DateTime now = DateTime.Now;
              //  ApplicationData.Current.LocalSettings.Values["DateCreated"] = now.ToString() ;
                ApplicationData.Current.LocalSettings.Values["Firstrun"] = "1";
                ApplicationData.Current.LocalSettings.Values["RunTime"] = 1;
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var messageDialog = new MessageDialog("Fill your name and picture and press agian :)");
                messageDialog.ShowAsync();
            }


        }

    }
}
