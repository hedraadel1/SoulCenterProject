using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_MessageEntities
    public class Soul_MessageEntities
    {
        #region Member Variables
        protected int _MessageID;
        protected int _EntityID;
        protected unknown _Relevance;
        #endregion
        #region Constructors
        public Soul_MessageEntities() { }
        public Soul_MessageEntities(unknown Relevance)
        {
            this._Relevance=Relevance;
        }
        #endregion
        #region Public Properties
        public virtual int MessageID
        {
            get {return _MessageID;}
            set {_MessageID=value;}
        }
        public virtual int EntityID
        {
            get {return _EntityID;}
            set {_EntityID=value;}
        }
        public virtual unknown Relevance
        {
            get {return _Relevance;}
            set {_Relevance=value;}
        }
        #endregion
    }
    #endregion
}