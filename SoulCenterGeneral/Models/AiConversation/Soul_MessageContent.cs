using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Soul_MessageContent
    {
        public int MessageContentID { get; set; }
        public int? MessageID { get; set; }
        public string MessageContent { get; set; }
        public string ContentType { get; set; }
    }
}
