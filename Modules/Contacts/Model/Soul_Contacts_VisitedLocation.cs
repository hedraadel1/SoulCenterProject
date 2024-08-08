﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Modules.Contacts.Model
{
    public class Soul_Contacts_VisitedLocation
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string LocationName { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitDescription { get; set; }
    }
}
