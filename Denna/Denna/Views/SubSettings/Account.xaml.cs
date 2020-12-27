using Core.Data;
using Core.Domain;
using Core.Service.Users;
using Core.Utils;
using Realms.Sync;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Account : Page
    {
        private UserService _usrsvc;
        public Account()
        {
            InitializeComponent();
            _usrsvc = new UserService();


            // reconnect, sync session
            // Get and Set user data
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                Username.Text = _usrsvc.GetUsername();
                DennaUser userInfo = _usrsvc.GetUserInfo();
                FullName.Text = userInfo.FullName;
                Email.Text = userInfo.Email;
            }
            catch
            {
                "Something isn't right".ShowMessage("Let us fetch your data from our cloud ...");
            }

            Ses.Text = RealmContext.GetInstance().GetSession().State.ToString();
            base.OnNavigatedTo(e);
        }

        private async void LogOut(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Log out? Seriously?");
            msg.Commands.Add(new UICommand("Yes", delegate
            {
                _usrsvc.Logout();
                Frame.Navigate(typeof(Welcome));
                Frame.BackStack.Clear();
            }));
            msg.Commands.Add(new UICommand("No", delegate { }));
            await msg.ShowAsync();

        }

        private void Reconnect_Click(object sender, RoutedEventArgs e)
        {
            Session.Reconnect();
        }

        private async void CoPAss_Click(object sender, RoutedEventArgs e)
        {


            MessageDialog msg = new MessageDialog("Changing password? Seriously?");
            msg.Commands.Add(new UICommand("Yes", async delegate
            {
                if (Pass.Text != Rpass.Text)
                {
                    "Retype password".ShowMessage("Passwords not maching");
                    return;
                }
                if (Pass.Text.Length < 6)
                {
                    "Passwork too short".ShowMessage("Passwords must have more than 6 chars");
                    return;
                }
                await _usrsvc.ChangePass(Pass.Text);
            }));
            msg.Commands.Add(new UICommand("No", delegate { }));
            await msg.ShowAsync();

        }

        private async void UsrInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Changing info? Seriously?");
            msg.Commands.Add(new UICommand("Yes", delegate
            {
                _usrsvc.UpdateUserInfo(Email.Text, FullName.Text);
            }));
            msg.Commands.Add(new UICommand("No", delegate { }));
            await msg.ShowAsync();






        }
    }
}