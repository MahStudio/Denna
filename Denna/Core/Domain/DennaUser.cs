using Realms;

namespace Core.Domain
{
    public class DennaUser : RealmObject
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
