using System.Threading.Tasks;
using Core.Data;
using Core.Domain;
using Realms.Sync;
using System.Linq;
using System;
using Core.Utils;
using Core.Service.Backwards;
using Core.Service.Notifications;

namespace Core.Service.Users
{
    public static class UserService
    {       
        static BackwardsService _backSvc = new BackwardsService();
        public static async Task Register(string username, string password, string name, string email)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: true);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            CreateUserInformation(name, email);
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

        public static void CreateUserInformation(string name, string email)
        {
            var usr = new DennaUser()
            {
                FullName = name,
                Email = email
            };
            RealmContext.GetInstance().Write(() =>
            {
                r.Add(usr);
            });
        }

        public static string GetUsername() => User.Current.Identity;
        public static Realms.Realm r = RealmContext.GetInstance();
        
        public static DennaUser GetUserInfo() => r.All<DennaUser>().FirstOrDefault();

        public static void UpdateUserInfo(DennaUser usr, DennaUser newUser)
        {

            try
            {
  
                
                r.Write(() =>
                {
                    usr.Email = newUser.Email;
                    usr.FullName = newUser.FullName;
                    r.Add(usr, update: true);
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