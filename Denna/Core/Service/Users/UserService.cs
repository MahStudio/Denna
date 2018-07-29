using Core.Data;
using Core.Domain;
using Realms.Sync;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Users
{
    public static class UserService
    {
        public static async Task Register(string username, string password, string name, string email)
        {
            var credentials = Credentials.UsernamePassword(username, password, createUser: true);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
            RealmContext.Initialize();
            CreateUserInformation(name, email);
        }
        public static async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username, password, createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
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
            RealmContext.Instance.Write(() =>
            {
                RealmContext.Instance.Add(usr);
            });
        }
        public static string GetUsername() => User.Current.Identity;
        public static DennaUser GetUserInfo() => RealmContext.Instance.All<DennaUser>().FirstOrDefault();

        public static void UpdateUserInfo(DennaUser usr, DennaUser newUser)
        {
            RealmContext.Instance.Write(() =>
            {
                usr.Email = newUser.Email;
                usr.FullName = newUser.FullName;
                RealmContext.Instance.Add(usr, update: true);
            });
        }
        public static async Task ChangePass(string newPass)
        {
            var currentUser = User.Current;
            await currentUser.ChangePasswordAsync(newPass);
        }
    }
}
