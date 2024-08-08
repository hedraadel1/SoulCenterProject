using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Soul_Memory
    {
        public int MemoryID { get; set; }
        public int ConversationID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
