using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_Message
    public class Soul_Message
    {
        #region Member Variables
        protected int _MessageID;
        protected string _MessageType;
        protected int _ConversationID;
        protected string _MessageSenderType;
        protected string _MessageSenderName;
        protected DateTime _MessageDatetime;
        protected int _MessageWordsCount;
        protected int _MessageContentID;
        protected int _RepliedToMessageID;
        #endregion
        #region Constructors
        public Soul_Message() { }
        public Soul_Message(string MessageType, int ConversationID, string MessageSenderType, string MessageSenderName, DateTime MessageDatetime, int MessageWordsCount, int MessageContentID, int RepliedToMessageID)
        {
            this._MessageType=MessageType;
            this._ConversationID=ConversationID;
            this._MessageSenderType=MessageSenderType;
            this._MessageSenderName=MessageSenderName;
            this._MessageDatetime=MessageDatetime;
            this._MessageWordsCount=MessageWordsCount;
            this._MessageContentID=MessageContentID;
            this._RepliedToMessageID=RepliedToMessageID;
        }
        #endregion
        #region Public Properties
        public virtual int MessageID
        {
            get {return _MessageID;}
            set {_MessageID=value;}
        }
        public virtual string MessageType
        {
            get {return _MessageType;}
            set {_MessageType=value;}
        }
        public virtual int ConversationID
        {
            get {return _ConversationID;}
            set {_ConversationID=value;}
        }
        public virtual string MessageSenderType
        {
            get {return _MessageSenderType;}
            set {_MessageSenderType=value;}
        }
        public virtual string MessageSenderName
        {
            get {return _MessageSenderName;}
            set {_MessageSenderName=value;}
        }
        public virtual DateTime MessageDatetime
        {
            get {return _MessageDatetime;}
            set {_MessageDatetime=value;}
        }
        public virtual int MessageWordsCount
        {
            get {return _MessageWordsCount;}
            set {_MessageWordsCount=value;}
        }
        public virtual int MessageContentID
        {
            get {return _MessageContentID;}
            set {_MessageContentID=value;}
        }
        public virtual int RepliedToMessageID
        {
            get {return _RepliedToMessageID;}
            set {_RepliedToMessageID=value;}
        }
        #endregion
    }
    #endregion
}