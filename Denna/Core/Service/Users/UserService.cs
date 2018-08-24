using System.Threading.Tasks;
using Core.Data;
using Core.Domain;
using Realms.Sync;
using System.Linq;
using System;
using Core.Utils;
using Core.Service.Backwards;
using Core.Service.Notifications;
using Realms;

namespace Core.Service.Users
{
    public static class UserService
    {
        static BackwardsService _backSvc = new BackwardsService();
        public static Realm _instance = RealmContext.GetInstance();
        public static async Task Register(string username, string password, string name, string email)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: true);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            await Task.Delay(200);
            CreateUserInformation(name, email, username);
            FinalizeLogin();
        }

        public static async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            FinalizeLogin();

        }
        static void FinalizeLogin()
        {
            if (_backSvc.IsBacwardsPresent())
                _backSvc.MigrateTodos();
        }

        public static async void Logout()
        {
            await User.Current.LogOutAsync();
            NotificationService.ClearBadgeAndLiveTile();
        }

        public static bool IsUserLoggenIn() => User.AllLoggedIn.Any();

        public static void CreateUserInformation(string name, string email, string username)
        {
            var usr = new DennaUser()
            {
                FullName = name,
                Email = email,
                Username = username
            };
            _instance.Write(() =>
            {
                _instance.Add(usr);
            });
        }

        public static string GetUsername()
        {
            var usr = GetUserInfo();
            if (string.IsNullOrEmpty(usr.Username))
                return User.Current.Identity;

            else return usr.Username;
        }


        public static DennaUser GetUserInfo() => _instance.All<DennaUser>().FirstOrDefault();

        public static void UpdateUserInfo(DennaUser usr, DennaUser newUser)
        {

            try
            {
                _instance.Write(() =>
                {
                    usr.Email = newUser.Email;
                    usr.FullName = newUser.FullName;
                    _instance.Add(usr, update: true);
                });
            }
            catch (Exception e)
            {
                e.Message.ShowMessage("error");
            }

        }

        public static async Task ChangePass(string newPass)
        {
            var currentUser = User.Current;
            await currentUser.ChangePasswordAsync(newPass);
        }
    }
}