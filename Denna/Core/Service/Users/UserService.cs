using Core.Data;
using Core.Domain;
using Core.Service.Notifications;
using Core.Utils;
using Realms;
using Realms.Sync;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Users
{
    public class UserService
    {
        public async Task Register(string username, string password, string name, string email)
        {
            Credentials credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: true);
            User user = await User.LoginAsync(credentials, Constants.ServerUri);
            await Task.Delay(200);
            CreateUserInformation(name, email, username);
        }

        public async Task Login(string username, string password)
        {
            Credentials credentials = Credentials.UsernamePassword(username.ToLower(), password, createUser: false);
            User user = await User.LoginAsync(credentials, Constants.ServerUri);

        }

        public async void Logout()
        {
            await User.Current.LogOutAsync();
            NotificationService.ClearBadgeAndLiveTile();
        }

        public bool IsUserLoggenIn()
        {
            return User.AllLoggedIn.Any();
        }

        public void CreateUserInformation(string name, string email, string username)
        {
            Realm instance = RealmContext.GetInstance();
            DennaUser usr = new DennaUser()
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
            DennaUser usr = GetUserInfo();
            if (string.IsNullOrEmpty(usr.Username))
            {
                return User.Current.Identity;
            }
            else
            {
                return usr.Username;
            }
        }


        public DennaUser GetUserInfo()
        {
            Realm instance = RealmContext.GetInstance();
            return instance.All<DennaUser>().FirstOrDefault();
        }

        public void UpdateUserInfo(string Email, string FullName)
        {
            DennaUser usr = GetUserInfo();
            if (usr.Email != Email || usr.FullName != FullName)
            {
                try
                {
                    Realm instance = usr.Realm;
                    instance.Write(() =>
                    {
                        usr.Email = Email;
                        usr.FullName = FullName;
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
            User currentUser = User.Current;
            await currentUser.ChangePasswordAsync(newPass);
        }
    }
}