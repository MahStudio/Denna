using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Types
{
    public class Person
    {

        public string Type { get; set; }


        public string ID { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }

        public string FirstName { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public Int64 Schema { get; set; }


    }
}
