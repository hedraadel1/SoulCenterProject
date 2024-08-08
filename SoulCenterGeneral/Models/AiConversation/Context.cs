using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulCenterProject.Models.AiConversation
{
    public class Context
    {
        public Dictionary<string, string> ContextItems { get; set; } = new Dictionary<string, string>();
    }
}
