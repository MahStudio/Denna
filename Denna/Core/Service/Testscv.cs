using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public static class Testscv
    {
        public static async void test()
        {
            var credentials = Credentials.UsernamePassword("mohsens22", "test", createUser: false);
            var user = await User.LoginAsync(credentials, Constants.ServerUri);
        }
    }
}
