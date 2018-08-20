using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Auth
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
