using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class DennaUser : RealmObject
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
