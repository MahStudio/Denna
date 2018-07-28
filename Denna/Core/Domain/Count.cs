using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    class Count:RealmObject
    {
        public RealmInteger<int> Counter { get; set; }
    }
}
