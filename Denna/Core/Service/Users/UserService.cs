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
    public class UserService
    {
        public async Task Register(string username, string password, string name, string email)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: true);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            await Task.Delay(200);
            CreateUserInformation(name, email, username);
            FinalizeLogin();
        }

        public async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            FinalizeLogin();

        }
        void FinalizeLogin()
        {
            var backSvc = new BackwardsService();

            if (backSvc.IsBacwardsPresent())
                backSvc.MigrateTodos();
        }

        public async void Logout()
        {
            await User.Current.LogOutAsync();
            NotificationService.ClearBadgeAndLiveTile();
        }

        public bool IsUserLoggenIn() => User.AllLoggedIn.Any();

        public void CreateUserInformation(string name, string email, string username)
        {
            var instance = RealmContext.GetInstance();
            var usr = new DennaUser()
            {
                FullName = name,
                Email = email,
                Username = username
            };
            instance.Write(() =>
            {
                instance.Add(usr);
            });
        }

        public string GetUsername()
        {
            var usr = GetUserInfo();
            if (string.IsNullOrEmpty(usr.Username))
                return User.Current.Identity;

            else return usr.Username;
        }


        public DennaUser GetUserInfo()
        {
            var instance = RealmContext.GetInstance();
            return instance.All<DennaUser>().FirstOrDefault();
        }

        public void UpdateUserInfo(DennaUser usr, DennaUser newUser)
        {
            if (usr.Email != newUser.Email || usr.FullName != newUser.FullName)
            {
                try
                {
                    var instance = RealmContext.GetInstance();
                    instance.Write(() =>
                    {
                        usr.Email = newUser.Email;
                        usr.FullName = newUser.FullName;
                        instance.Add(usr, update: true);
                    });
                }
                catch (Exception e)
                {
                    "Error".ShowMessage(e.Message);
                }
            }
            

        }

        public async Task ChangePass(string newPass)
        {
            var currentUser = User.Current;
            await currentUser.ChangePasswordAsync(newPass);
        }
    }
}