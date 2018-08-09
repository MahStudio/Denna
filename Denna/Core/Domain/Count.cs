using Realms;

namespace Core.Domain
{
    class Count : RealmObject
    {
        public RealmInteger<int> Counter { get; set; }
    }
}