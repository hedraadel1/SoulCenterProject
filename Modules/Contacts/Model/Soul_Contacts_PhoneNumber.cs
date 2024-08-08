using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Contacts.Model
{
    public class Soul_Contacts_PhoneNumber
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberType { get; set; }
    }
}
