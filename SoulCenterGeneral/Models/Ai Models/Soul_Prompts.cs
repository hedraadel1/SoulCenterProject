using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_Prompts
    public class Soul_Prompts
    {
        #region Member Variables
        protected int _PromptID;
        protected string _PromptName;
        protected string _PromptDescription;
        protected int _CategoryID;
        protected string _FullPromptText;
        protected DateTime _CreatedDate;
        protected string _CreatedBy;
        #endregion
        #region Constructors
        public Soul_Prompts() { }
        public Soul_Prompts(string PromptName, string PromptDescription, int CategoryID, string FullPromptText, DateTime CreatedDate, string CreatedBy)
        {
            this._PromptName=PromptName;
            this._PromptDescription=PromptDescription;
            this._CategoryID=CategoryID;
            this._FullPromptText=FullPromptText;
            this._CreatedDate=CreatedDate;
            this._CreatedBy=CreatedBy;
        }
        #endregion
        #region Public Properties
        public virtual int PromptID
        {
            get {return _PromptID;}
            set {_PromptID=value;}
        }
        public virtual string PromptName
        {
            get {return _PromptName;}
            set {_PromptName=value;}
        }
        public virtual string PromptDescription
        {
            get {return _PromptDescription;}
            set {_PromptDescription=value;}
        }
        public virtual int CategoryID
        {
            get {return _CategoryID;}
            set {_CategoryID=value;}
        }
        public virtual string FullPromptText
        {
            get {return _FullPromptText;}
            set {_FullPromptText=value;}
        }
        public virtual DateTime CreatedDate
        {
            get {return _CreatedDate;}
            set {_CreatedDate=value;}
        }
        public virtual string CreatedBy
        {
            get {return _CreatedBy;}
            set {_CreatedBy=value;}
        }
        #endregion
    }
    #endregion
}