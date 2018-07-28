using Realms.Sync;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Users
{
    public static class UserService
    {
        public static async Task Register(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username, password, createUser: true);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);

        }
        public static async Task Login(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username, password, createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);
        }
        public static async void Logout() => await User.Current.LogOutAsync();

        public static bool IsUserLoggenIn() => User.AllLoggedIn.Any();

    }
}
