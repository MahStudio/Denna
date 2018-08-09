using Newtonsoft.Json;
using Planel.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Globalization;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
   // (bool)ApplicationData.Current.LocalSettings.Values["Showtoast"] == true
    public sealed partial class Settings : Page
    {
        public static bool isloaded;
        ObservableCollection<Core.Models.todo> todos = new ObservableCollection<Core.Models.todo>();
        public Settings()
        {
            InitializeComponent();
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
            App_BackRequested;
            todos = Core.Models.Localdb.getlist();
            try
            {
                if (Convert.ToBoolean(ApplicationData.Current.LocalSettings.Values["FollowAccent"]) == true)
                    FollowAccent.IsOn = true;
                else
                    FollowAccent.IsOn = false;
            }
            catch
            { }
            try
            {
                if (Classes.Themesetter.GetApplicationTheme() == "Dark")
                    ThemeSelector.SelectedIndex = 0;
                else if (Classes.Themesetter.GetApplicationTheme() == "Light")
                    ThemeSelector.SelectedIndex = 1;
                else
                    ThemeSelector.SelectedIndex = 2;
            }
            catch
            { }
            if (MultilingualHelpToolkit.GetString("Language", "Tag") == "fa")
            {
                langwich.SelectedIndex = 0;
                Translator.Text = " ";
            }
            else if (MultilingualHelpToolkit.GetString("Language", "Tag") == "en-us")
            {
                langwich.SelectedIndex = 1;
                Translator.Text = " ";
            }
            else if (MultilingualHelpToolkit.GetString("Language", "Tag") == "de-de")
            {
                langwich.SelectedIndex = 2;
                Translator.Text = "By Lukas Frensel";
            }
            else if (MultilingualHelpToolkit.GetString("Language", "Tag") == "nl-nl")
            {
                langwich.SelectedIndex = 3;
                Translator.Text = "By Rick Drijvers";
            }
        }
        ~Settings()
        {
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -=
             App_BackRequested;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((bool)ApplicationData.Current.LocalSettings.Values["Showtoast"] == true)
            {
                swicher.IsOn = true;
            }
            else
            {
                swicher.IsOn = false;
            }

            base.OnNavigatedTo(e);
            var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
            if (args != null)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
                {
                    isloaded = true;
                    var fileArgs = args as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                    var strFilePath = fileArgs.Files[0].Path;
                    var file = (StorageFile)fileArgs.Files[0];
                    var json = await FileIO.ReadTextAsync(file);
                    var toadd = JsonConvert.DeserializeObject<IList<Core.Models.todo>>(json);
                    List<Core.Models.todo> adder = new List<Core.Models.todo>();
                    adder = toadd.ToList();
                    var msg = new MessageDialog(MultilingualHelpToolkit.GetString("Restoree", "Text"));
                    msg.Commands.Add(new UICommand("Yes", async delegate
                    {
                        foreach (var item in toadd)
                            await Core.Models.Localdb.Addtodo(item);


                        worker.refresher("");
                        var noWifiDialog = new ContentDialog()
                        {
                            Title = "Success! :)",
                            Content = "Backup had been restored.",
                            PrimaryButtonText = "Nice!"
                        };
                        await noWifiDialog.ShowAsync();
                    }));
                    msg.Commands.Add(new UICommand("Nope"));
                    msg.ShowAsync();
                }
            }
            // try
            // {
            //    if (App.licenseactive == true)
            //    {
            //        if (App.License.IsActive == true)
            //        {
            //            Licencer.Text = "License active WW";
            //        }
            //        if (App.License.IsActive == false)
            //        {
            //            Licencer.Text = "License active IR";
            //        }
            //    }
            //    else
            //    {
            //        Licencer.Text = "Trial";
            //        declame.Visibility = Visibility.Visible;
            //    }
            // }
            // catch
            // {
            //    Licencer.Text = "There was a problem with your licence activation";
            //    declame.Visibility = Visibility.Visible;
            // }

            var rootFrame = Window.Current.Content as Frame;

            var myPages = "";
            foreach (PageStackEntry page in rootFrame.BackStack)
                myPages += page.SourcePageType.ToString() + "\n";


            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }

            namebox.Text = ApplicationData.Current.LocalSettings.Values["Username"].ToString();
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.GetFileAsync("avatar.jpg");

            avatar.ImageSource = new BitmapImage(new Uri(sampleFile.Path));
        }

        string name, filename;

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
                    sampleFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
                catch { }

                var copiedFile = await file.CopyAsync(localFolder, "avatar.jpg");

                filename = "avatar.jpg";

                avatar.ImageSource = null;

                const uint SIZE = 150; //Send your required size
                using (StorageItemThumbnail thumbnail = await copiedFile.GetThumbnailAsync(ThumbnailMode.SingleItem, SIZE))
                {
                    if (thumbnail != null)
                    {
                        // Prepare thumbnail to display
                        var bitmapImage = new BitmapImage();

                        bitmapImage.SetSource(thumbnail);
                        avatar.ImageSource = bitmapImage;
                    }
                }

                var messageDialog = new MessageDialog("Your picture had been saved successfuly.");
                messageDialog.ShowAsync();
            }
        }

        void namebox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = namebox.Text;
        }

        async void logout_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog(MultilingualHelpToolkit.GetString("Shor", "Text"));
            msg.Commands.Add(new UICommand("Yes", async delegate
            {
                bye.Visibility = Visibility.Visible;

                await Task.Delay(3000);

                Core.Models.Localdb.Logout();
            }));
            msg.Commands.Add(new UICommand("No"


            ));
            msg.ShowAsync();
        }

        async void Buy_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?productid=9n9c2hwnzcft"));
        }

        void Iran_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(IranBye));

        async void saver_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Core.Models.todo> forsave = new ObservableCollection<Core.Models.todo>();
            forsave = todos;
            var json = JsonConvert.SerializeObject(forsave);
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Denna backup file", new List<string>() { ".djson" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "DennaBackup";
            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                await Windows.Storage.FileIO.WriteTextAsync(file, json);
                var status =
           await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    var noWifiDialog = new ContentDialog()
                    {
                        Title = "Success!",
                        Content = "Backup had been saved .",
                        PrimaryButtonText = "Nice!"
                    };
                    noWifiDialog.ShowAsync();
                }
                else
                {
                    var noWifiDialog = new ContentDialog()
                    {
                        Title = ":(",
                        Content = "Something went wrong",
                        PrimaryButtonText = "OK"
                    };
                    noWifiDialog.ShowAsync();
                }
            }
            else
            {
                var noWifiDialog = new ContentDialog()
                {
                    Title = ":/",
                    Content = "Operation canceled",
                    PrimaryButtonText = "OK"
                };
                noWifiDialog.ShowAsync();
            }
        }

        async void pick_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".djson");
            IStorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file

                // try
                // {

                var json = await FileIO.ReadTextAsync(file);
                var toadd = JsonConvert.DeserializeObject<IList<Core.Models.todo>>(json);
                List<Core.Models.todo> adder = new List<Core.Models.todo>();
                adder = toadd.ToList();
                foreach (var item in toadd)
                    await Core.Models.Localdb.Addtodo(item);


                worker.refresher("");
                var noWifiDialog = new ContentDialog()
                {
                    Title = "Success!",
                    Content = "Backup had been restored.",
                    PrimaryButtonText = "Nice!"
                };
                await noWifiDialog.ShowAsync();
                // }
                // catch { }
            }
        }

        async void swicher_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    ApplicationData.Current.LocalSettings.Values["Showtoast"] = true;
                    Core.Classes.LiveTile.GenerateToast();
                }
                else
                {
                    ApplicationData.Current.LocalSettings.Values["Showtoast"] = false;
                }
            }
        }

        void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.Themesetter.SetApplicationTheme((ThemeSelector.SelectedItem as ComboBoxItem).Tag.ToString());
            }
            catch { }
        }

        void FollowAccent_Toggled(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FollowAccent.IsOn)
                    ApplicationData.Current.LocalSettings.Values["FollowAccent"] = true;
                else
                    ApplicationData.Current.LocalSettings.Values["FollowAccent"] = false;
            }
            catch
            {
            }
        }

        void langwich_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (langwich.SelectedIndex == 0)
            {
                var culture = new CultureInfo("fa");
                ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                Translator.Text = " ";
            }
            else if (langwich.SelectedIndex == 1)
            {
                var culture = new CultureInfo("en-us");
                ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                Translator.Text = " ";
            }
            else if (langwich.SelectedIndex == 2)
            {
                var culture = new CultureInfo("de-de");
                ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                Translator.Text = "By Lukas Frensel";
            }
            else if (langwich.SelectedIndex == 3)
            {
                var culture = new CultureInfo("nl-nl");
                ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                Translator.Text = "By Rick Drijvers";
            }
        }
    }
}