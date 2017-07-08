using System;
using System.Collections.Generic;
using System.Text;
using CoreDenna.Types;
using Couchbase.Lite;

namespace CoreDenna.Models
{
    public static class UserModel
    {
        public static void AddUser(User usr)
        {
            
            Dictionary<string, object> Dic = new Dictionary<string, object>() {
                { nameof(User.Email) , usr.Email },
                 { nameof(User.Type) , nameof(User) },
                  { nameof(User.FirstName) , usr.FirstName },
                   { nameof(User.LastName) , usr.LastName },
                  { nameof(User.Password) , usr.Password },
                  { nameof(User.CreatedAt) , DateTime.Now },
                   { nameof(User.Schema) , 1}


            }
            ;
            DBAccess.CreateDoc(Dic);
           
            
        }
    }
}
