using Core.Data;
using Core.Domain;
using Core.Service.Users;
using Core.Utils;
using Realms.Sync;
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
        UserService _usrsvc;
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
                var userInfo = _usrsvc.GetUserInfo();
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

        void LogOut(object sender, RoutedEventArgs e)
        {
            _usrsvc.Logout();
            Frame.Navigate(typeof(Welcome));
            Frame.BackStack.Clear();
        }

        void Reconnect_Click(object sender, RoutedEventArgs e) => Session.Reconnect();

        async void CoPAss_Click(object sender, RoutedEventArgs e)
        {
            if (Pass.Text != Rpass.Text)
            {
                "Retype password".ShowMessage("Passwords not maching");
                return;
            }

            await _usrsvc.ChangePass(Pass.Text);
        }

        void UsrInfo_Click(object sender, RoutedEventArgs e)
        {

            var user = _usrsvc.GetUserInfo();
            var UpdatedInfo = new DennaUser();

            UpdatedInfo.Email = Email.Text;
            UpdatedInfo.FullName = FullName.Text;



            _usrsvc.UpdateUserInfo(_usrsvc.GetUserInfo(), UpdatedInfo);
        }
    }
}