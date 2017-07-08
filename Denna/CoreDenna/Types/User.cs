using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDenna.Types
{
    public class User
    {
        public string Type { get; set; }
        public string ID { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Schema { get; set; }
    }
}
