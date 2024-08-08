using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Contacts.Model
{
    public class Soul_Contacts_SharedPerson
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int SharedPersonID { get; set; }
        public string RelationshipType { get; set; }
    }
}
