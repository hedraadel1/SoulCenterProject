using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_Conversation
    public class Soul_Conversation
    {
        #region Member Variables
        protected int _ConversationID;
        protected DateTime _StartDateTime;
        protected int _MessagesCount;
        #endregion
        #region Constructors
        public Soul_Conversation() { }
        public Soul_Conversation(DateTime StartDateTime, int MessagesCount)
        {
            this._StartDateTime=StartDateTime;
            this._MessagesCount=MessagesCount;
        }
        #endregion
        #region Public Properties
        public virtual int ConversationID
        {
            get {return _ConversationID;}
            set {_ConversationID=value;}
        }
        public virtual DateTime StartDateTime
        {
            get {return _StartDateTime;}
            set {_StartDateTime=value;}
        }
        public virtual int MessagesCount
        {
            get {return _MessagesCount;}
            set {_MessagesCount=value;}
        }
        #endregion
    }
    #endregion
}