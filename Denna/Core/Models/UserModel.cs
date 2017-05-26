using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using Windows.UI.Xaml.Media.Imaging;
using System.Reflection;

namespace Core.Models
{
    public static class MyExtensions
    {
        public static Dictionary<string, object> ToDictionary(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new { Name = pi.Name, Value = pi.GetValue(myObj, null) })
                .Union(
                    myObj.GetType()
                    .GetFields()
                    .Select(fi => new { Name = fi.Name, Value = fi.GetValue(myObj) })
                 )
                .ToDictionary(ks => ks.Name, vs => vs.Value);
        }
     
    }
    public class UserModel
    {
        

       public static async Task CreateUser(string ID, string email, string Name, string Family, string pass)
        {
            var dds = new Types.Person
            {
                ID = ID,
                Email = email,
                Type = "User",

                FirstName = Name,
                LastName = Family,
                Password = pass,
                CreatedAt = DateTime.UtcNow.ToString(),
                Schema = 1
            
            }
            ;
            var mydic = dds.ToDictionary();
            IDictionary<string, object> an = mydic;
              DBH.MakeDoc(an);

        }
        

        public static string GetName()
        {
           
            return DBH.Query();
        }
    }
}
