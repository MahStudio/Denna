using System.Threading.Tasks;
using Core.Data;
using Core.Domain;
using Realms.Sync;
using System.Linq;
using System;
using Core.Utils;

namespace Core.Service.Users
{
    public static class UserService
    {
        public static async Task Register(string username, string password, string name, string email)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: true);
            try
            {
                var user = await User.LoginAsync(credentials, Constants.ServerUri);
                User.ConfigurePersistence(UserPersistenceMode.Encrypted);
                CreateUserInformation(name, email);
            }
            catch (Exception ex)
            {
                "SomethingwentWrong".ShowMessage(ex.Message);
            }

        }

        public static async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: false);
            try
            {
                var user = await User.LoginAsync(credentials, Constants.ServerUri);
                User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            }
            catch (Exception ex)
            {
                "SomethingwentWrong".ShowMessage(ex.Message);
            }
        }

        public static async void Logout() => await User.Current.LogOutAsync();

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
                RealmContext.GetInstance().Add(usr);
            });
        }

        public static string GetUsername() => User.Current.Identity;

        public static DennaUser GetUserInfo() => RealmContext.GetInstance().All<DennaUser>().FirstOrDefault();

        public static void UpdateUserInfo(DennaUser usr, DennaUser newUser)
        {
            RealmContext.GetInstance().Write(() =>
            {
                usr.Email = newUser.Email;
                usr.FullName = newUser.FullName;
                RealmContext.GetInstance().Add(usr, update: true);
            });
        }

        public static async Task ChangePass(string newPass)
        {
            var currentUser = User.Current;
            await currentUser.ChangePasswordAsync(newPass);
        }
    }
}