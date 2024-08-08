namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_EnvironmentConversations
    public class Soul_EnvironmentConversations
    {
        #region Member Variables
        protected int _EnvironmentConversationID;
        protected int _EnvironmentID;
        protected int _ConversationID;
        #endregion
        #region Constructors
        public Soul_EnvironmentConversations() { }
        public Soul_EnvironmentConversations(int EnvironmentID, int ConversationID)
        {
            this._EnvironmentID=EnvironmentID;
            this._ConversationID=ConversationID;
        }
        #endregion
        #region Public Properties
        public virtual int EnvironmentConversationID
        {
            get {return _EnvironmentConversationID;}
            set {_EnvironmentConversationID=value;}
        }
        public virtual int EnvironmentID
        {
            get {return _EnvironmentID;}
            set {_EnvironmentID=value;}
        }
        public virtual int ConversationID
        {
            get {return _ConversationID;}
            set {_ConversationID=value;}
        }
        #endregion
    }
    #endregion
}