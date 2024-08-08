using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SoulCenterProject.Models.Soul_Models
{
    public class Soul_Agents
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgentApiID { get; set; }
        public string AgentModelID { get; set; }
        public int AgentMaxOutput { get; set; }
        public int AgentMaxInput { get; set; }
        public string AgentHARMCATEGORYHARASSMENT { get; set; }
        public string AgentHARMCATEGORYHATESPEECH { get; set; }
        public string AgentHARMCATEGORYSEXUALLYEXPLICIT { get; set; }
        public string AgentHARMCATEGORYDANGEROUSCONTENT { get; set; }
        public string AgentType { get; set; }
        public string AgentLanguage { get; set; }
        public string AgentCapabilities { get; set; }
        public string AgentSettings { get; set; }
        public string DefaultConverstion { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AgentDescription { get; set; }
        public string AgentAvatar { get; set; }
    }
}