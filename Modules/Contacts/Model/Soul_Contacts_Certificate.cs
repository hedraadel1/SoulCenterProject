using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Contacts.Model
{
    public class Soul_Contacts_Certificate
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string CertificateName { get; set; }
        public string IssuingAuthority { get; set; }
        public DateTime? DateObtained { get; set; }
    }
}
