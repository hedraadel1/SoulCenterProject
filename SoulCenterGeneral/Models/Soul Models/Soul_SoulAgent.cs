using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoulCenterProject.Models.Soul_Models;

namespace SoulCenterProject.Models.Soul_Models
{
    public class Soul_SoulAgent
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgentApiID { get; set; }
        public string AgentModelID { get; set; }
        public int AgentMaxOutput { get; set; }
        public int AgentMaxInput { get; set; }
        public string Agent_HARM_CATEGORY_HARASSMENT { get; set; }
        public string Agent_HARM_CATEGORY_HATE_SPEECH { get; set; }
        public string Agent_HARM_CATEGORY_SEXUALLY_EXPLICIT { get; set; }
        public string Agent_HARM_CATEGORY_DANGEROUS_CONTENT { get; set; }
        public string AgentType { get; set; }
        public string AgentLanguage { get; set; }
        public string AgentCapabilities { get; set; }
        public string AgentSettings { get; set; }
        public string DefaultConverstion { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AgentDescription { get; set; }
        public string AgentAvatar { get; set; }


        // Navigation properties
        public virtual Soul_ApiKeyInfo ApiKeyInfo { get; set; }
        public virtual SoulModelInfo ModelInfo { get; set; }
    }
}