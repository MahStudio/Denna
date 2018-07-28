using Denna.Classes;
using Denna.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Core.Service.Users;
using Core.Data;

namespace Denna.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        private string _password;
        private string _rpassword;
        private string _email;
        private string _name;
        private string _family;
        private BitmapImage _avatar;
        public BitmapImage Avatar
        {
            get
            {
                return _avatar;

            }
            set
            {

                if (_avatar != value)
                {
                    _avatar = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Avatar"));
                    }
                }
            }
        }
        public string Family
        {
            get
            {
                return _family;

            }
            set
            {

                if (_family != value)
                {
                    _family = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Family"));
                    }
                }
            }
        }
        public string Name
        {
            get
            {
                return _name;

            }
            set
            {

                if (_name != value)
                {
                    _name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }
        public string Email
        {
            get
            {
                return _email;

            }
            set
            {

                if (_email != value)
                {
                    _email = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Email"));
                    }
                }
            }
        }
        public string RPassword
        {
            get
            {
                return _rpassword;

            }
            set
            {

                if (_rpassword != value)
                {
                    _rpassword = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("RPassword"));
                    }
                }
            }
        }
        public string UserName
        {
            get
            {
                return _username;

            }
            set
            {

                if (_username != value)
                {
                    _username = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("UserName"));
                    }
                }
            }
        }
        public string Password
        {
            get
            {
                return _password;

            }
            set
            {

                if (_password != value)
                {
                    _password = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Password"));
                    }
                }
            }
        }
        public MyCommand SignInCommand
        {
            get;
            set;
        }
        public MyCommand SignUpCommand
        {
            get;
            set;
        }
        public MyCommand Picture
        {
            get;
            set;
        }
        public SignUpViewModel()
        {
            SignInCommand = new MyCommand();
            SignInCommand.CanExecuteFunc = obj => true;
            SignInCommand.ExecuteFunc = SignIn;
            SignUpCommand = new MyCommand();
            SignUpCommand.CanExecuteFunc = obj => true;
            SignUpCommand.ExecuteFunc = SignUp;
            Picture = new MyCommand();
            Picture.CanExecuteFunc = obj => true;
            Picture.ExecuteFunc = PictureeAsync;

        }



        private string name;
        private string filename;

        private async void PictureeAsync(object obj)
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




                const uint size = 150; //Send your required size
                using (StorageItemThumbnail thumbnail = await copiedFile.GetThumbnailAsync(ThumbnailMode.SingleItem, size))
                {
                    if (thumbnail != null)
                    {
                        //Prepare thumbnail to display
                        BitmapImage bitmapImage = new BitmapImage();

                        bitmapImage.SetSource(thumbnail);
                        Avatar = bitmapImage;


                    }
                }
                var messageDialog = new MessageDialog("Your picture had been saved successfuly.");
                messageDialog.ShowAsync();

            }
        }
        private async void SignUp(object obj)
        {
            if (Password != RPassword)
            {
                "Retype password".ShowMessage("Passwords not maching");
                return;
            }

            await UserService.Register(UserName, Password);
            RealmContext.Initialize();
            Welcome.current.Frame.Navigate(typeof(PageMaster));
        }

        private void SignIn(object obj)
        {
            Welcome.current.opensignin();
        }
    }
}
