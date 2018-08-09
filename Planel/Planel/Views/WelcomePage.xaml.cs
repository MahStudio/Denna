using Planel.Classes;
using System;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
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
            InitializeComponent();
            Core.Models.Localdb.CreateDatabase();
            filsupress();
            coloradjust();
        }
        void coloradjust()
        {
            var a = (Color)Application.Current.Resources["SystemAccentColor"];
            try
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = a;
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = a;
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.InactiveBackgroundColor = a;
                titleBar.ButtonInactiveBackgroundColor = a;
                titleBar.InactiveForegroundColor = Colors.White;
                titleBar.ButtonInactiveForegroundColor = Colors.White;
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonForegroundColor = Colors.White;
                // fuck you asshilism
            }
            catch
            {
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = a;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }

        async void filsupress()
        {
            Superss.Source = new BitmapImage(new Uri("ms-appx:///Assets/Headings/h14.png"));
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

            if (flipwel.SelectedIndex == 4)
            {
                m5();
            }
        }

        void b1_Click(object sender, RoutedEventArgs e) => m1();

        void b2_Click(object sender, RoutedEventArgs e) => m2();

        void b3_Click(object sender, RoutedEventArgs e) => m3();

        void b4_Click(object sender, RoutedEventArgs e) => m4();

        void b5_Click(object sender, RoutedEventArgs e) => m5();

        void m1()
        {
            b1.Background = new SolidColorBrush(Colors.Gray);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 0;
        }

        void m2()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.Gray);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 1;
        }

        void m3()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.Gray);
            b4.Background = new SolidColorBrush(Colors.White);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 2;
        }

        void m4()
        {
            b1.Background = new SolidColorBrush(Colors.White);
            b2.Background = new SolidColorBrush(Colors.White);
            b3.Background = new SolidColorBrush(Colors.White);
            b4.Background = new SolidColorBrush(Colors.Gray);
            b5.Background = new SolidColorBrush(Colors.White);
            flipwel.SelectedIndex = 3;
        }

        void m5()
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

        string name;
        string filename;

        async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                var localFolder = ApplicationData.Current.LocalFolder;

                try
                {
                    var storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
                    var sampleFile =
                        await storageFolder.GetFileAsync("avatar.jpg");
                    await sampleFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
                catch { }

                var copiedFile = await file.CopyAsync(localFolder, "avatar.jpg");
                filename = "avatar.jpg";
                var storageFolder2 = ApplicationData.Current.LocalFolder;
                var sampleFile2 = await storageFolder2.GetFileAsync("avatar.jpg");

                avatar.ImageSource = null;
                const uint SIZE = 150; //Send your required size
                using (StorageItemThumbnail thumbnail = await sampleFile2.GetThumbnailAsync(ThumbnailMode.SingleItem, SIZE))
                {
                    if (thumbnail != null)
                    {
                        // Prepare thumbnail to display
                        var bitmapImage = new BitmapImage();

                        bitmapImage.SetSource(thumbnail);
                        avatar.ImageSource = bitmapImage;
                        butt1.Content = "";
                    }
                }
            }
        }

        async void Button_Click(object sender, RoutedEventArgs e)
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

            if (name != null)
            {
                Core.Models.Localdb.Iuser(name);

                if (filename == null)
                {
                    try
                    {
                        var storageFolder =
        Windows.Storage.ApplicationData.Current.LocalFolder;
                        var sampleFile =
                            await storageFolder.GetFileAsync("avatar.jpg");
                        await sampleFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                    }
                    catch { }
                    var CountriesFile = @"Assets\Mockops\usrimg.jpg";
                    var InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                    var file = await InstallationFolder.GetFileAsync(CountriesFile);
                    var localFolder = ApplicationData.Current.LocalFolder;
                    var copiedFile = await file.CopyAsync(localFolder, "avatar.jpg");
                }

                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var messageDialog = new MessageDialog(MultilingualHelpToolkit.GetString("Fillalert", "Text"));
                messageDialog.ShowAsync();
            }
        }
    }
}