using System.Threading.Tasks;
using Core.Data;
using Core.Domain;
using Realms.Sync;
using System.Linq;
using System;
using Core.Utils;
using Core.Service.Auth;

namespace Core.Service.Users
{
    public class UserService
    {
        public async Task Register(string username, string password, string name, string email)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: true);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            CreateUserInformation(name, email);

        }

        public async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);

        }
        public async Task LoginWithMicrosoft(string username, string password)
        {
            var msUser = await MicrosoftLogin.AuthMicrosoft();
            var credentials = Credentials.Custom("MicrosoftAccount", msUser.Token, null);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            if (!IsInformationPresent())
                CreateUserInformation(msUser.Name, msUser.Email);

        }

        public async void Logout() => await User.Current.LogOutAsync();

        public bool IsUserLoggenIn() => User.AllLoggedIn.Any();
        public bool IsInformationPresent() => RealmContext.GetInstance().All<DennaUser>().Any();

        public void CreateUserInformation(string name, string email)
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

        public string GetUsername() => User.Current.Identity;

        public DennaUser GetUserInfo() => RealmContext.GetInstance().All<DennaUser>().FirstOrDefault();

        public void UpdateUserInfo(DennaUser usr, DennaUser newUser)
        {
            RealmContext.GetInstance().Write(() =>
            {
                usr.Email = newUser.Email;
                usr.FullName = newUser.FullName;
                RealmContext.GetInstance().Add(usr, update: true);
            });
        }

        public async Task ChangePass(string newPass)
        {
            var currentUser = User.Current;
            await currentUser.ChangePasswordAsync(newPass);
        }
    }
}