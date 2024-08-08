using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Contacts.Model
{
    public class Soul_Contacts_Address
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string AddressDetails { get; set; }
        public string AddressType { get; set; }
    }
}
