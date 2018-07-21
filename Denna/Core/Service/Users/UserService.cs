using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Users
{
    public static class UserService
    {
        public static async void Register(string username, string password)
        {
            var credentials = Credentials.UsernamePassword(username, password, createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
            User.ConfigurePersistence(UserPersistenceMode.Encrypted);

        }
    }
}
