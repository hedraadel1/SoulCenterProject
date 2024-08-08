using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Contacts.Model
{
    public class Soul_Contacts_Person
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string MainPhoneNumber { get; set; }
        public string Religion { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
