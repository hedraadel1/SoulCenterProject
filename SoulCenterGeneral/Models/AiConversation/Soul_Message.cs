using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Soul_Message
    {
        public int MessageID { get; set; }
        public string MessageType { get; set; }
        public int? ConversationID { get; set; }
        public string MessageSenderType { get; set; }
        public string MessageSenderName { get; set; }
        public DateTime? MessageDatetime { get; set; }
        public int? MessageWordsCount { get; set; }
        public string MessageContent { get; set; }
        public int? RepliedToMessageID { get; set; }
        public bool SendToAiHistory { get; set; }
        public bool PinTheMessage { get; set; }

    
    }
}
