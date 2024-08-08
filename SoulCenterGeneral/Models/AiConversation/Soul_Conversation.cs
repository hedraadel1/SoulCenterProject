using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Soul_Conversationfalse
    {
        public int ConversationID { get; set; }
        public string ConversationName { get; set; } = "NOW()";
        public int AgentID { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int? MessagesCount { get; set; }
    }
}
