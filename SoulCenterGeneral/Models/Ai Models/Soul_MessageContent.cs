using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_MessageContent
    public class Soul_MessageContent
    {
        #region Member Variables
        protected int _MessageContentID;
        protected int _MessageID;
        protected string _MessageContent;
        protected string _ContentType;
        #endregion
        #region Constructors
        public Soul_MessageContent() { }
        public Soul_MessageContent(int MessageID, string MessageContent, string ContentType)
        {
            this._MessageID=MessageID;
            this._MessageContent=MessageContent;
            this._ContentType=ContentType;
        }
        #endregion
        #region Public Properties
        public virtual int MessageContentID
        {
            get {return _MessageContentID;}
            set {_MessageContentID=value;}
        }
        public virtual int MessageID
        {
            get {return _MessageID;}
            set {_MessageID=value;}
        }
        public virtual string MessageContent
        {
            get {return _MessageContent;}
            set {_MessageContent=value;}
        }
        public virtual string ContentType
        {
            get {return _ContentType;}
            set {_ContentType=value;}
        }
        #endregion
    }
    #endregion
}