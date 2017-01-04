using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
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
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            namebox.Text = ApplicationData.Current.LocalSettings.Values["Username"].ToString();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("avatar.jpg");

            avatar.ImageSource = new BitmapImage(new Uri(sampleFile.Path));
        }
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

                StorageFile copiedFile = await file.CopyAsync(localFolder, "avatar.jpg");
                
                filename = "avatar.jpg";
                

                avatar.ImageSource = null;
                avatar.ImageSource = new BitmapImage(new Uri(copiedFile.Path));
                var messageDialog = new MessageDialog("Your picture had been saved successfuly.");
                messageDialog.ShowAsync();

            }
        }

        private void namebox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = namebox.Text;
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
