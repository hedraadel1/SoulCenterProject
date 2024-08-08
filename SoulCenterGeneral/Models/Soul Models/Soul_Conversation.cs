using System;

namespace SoulCenterProject.Models.Soul_Models
{
    #region SoulConversation

    public class Soul_Conversation
    {
        public int ConversationID { get; set; }
        public string ConversationName { get; set; }
        public int AgentID { get; set; }
        public DateTime StartDateTime { get; set; }
        public int MessagesCount { get; set; }
    }

    #endregion
}