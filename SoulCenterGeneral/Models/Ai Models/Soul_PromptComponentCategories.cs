using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_PromptComponentCategories
    public class Soul_PromptComponentCategories
    {
        #region Member Variables
        protected int _CategoryID;
        protected string _CategoryName;
        #endregion
        #region Constructors
        public Soul_PromptComponentCategories() { }
        public Soul_PromptComponentCategories(string CategoryName)
        {
            this._CategoryName=CategoryName;
        }
        #endregion
        #region Public Properties
        public virtual int CategoryID
        {
            get {return _CategoryID;}
            set {_CategoryID=value;}
        }
        public virtual string CategoryName
        {
            get {return _CategoryName;}
            set {_CategoryName=value;}
        }
        #endregion
    }
    #endregion
}