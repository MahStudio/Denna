using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using Windows.UI.Xaml.Media.Imaging;

namespace Core.Models
{
    public class UserModel
    {
       public static async void CreateUser(string ID, string email, string Name, string Family, string pass)
        {
            
            
            Dictionary<string, object> mydic = new Dictionary<string, object>
            {
                ["Type"] = "user",
                ["ID"] = ID,
                ["Email"] = email,
                ["FirstName"] =Name ,
                ["LastName"] = Family,
                ["PassHash"] = pass,
                ["Created"] = DateTimeOffset.UtcNow,
                ["SchemaVers"] = 1
            }
            ;
            IDictionary<string, object> an = mydic;
              DBH.MakeDoc(an);

        }


        public static string GetName()
        {
           
            return DBH.Query();
        }
    }
}
