using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Soul_Entities
    {
        public int EntityID { get; set; }
        public string EntityType { get; set; }
        public string EntityValue { get; set; }
        public float? ConfidenceScore { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
