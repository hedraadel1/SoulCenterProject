using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Soul_MessageEntities
    {
        public int MessageID { get; set; }
        public int EntityID { get; set; }
        public float? Relevance { get; set; }
    }
}
