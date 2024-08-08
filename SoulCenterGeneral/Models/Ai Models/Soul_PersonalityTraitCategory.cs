using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_PersonalityTraitCategory
    public class Soul_PersonalityTraitCategory
    {
        #region Member Variables
        protected int _ID;
        protected string _CategoryName;
        #endregion
        #region Constructors
        public Soul_PersonalityTraitCategory() { }
        public Soul_PersonalityTraitCategory(string CategoryName)
        {
            this._CategoryName=CategoryName;
        }
        #endregion
        #region Public Properties
        public virtual int ID
        {
            get {return _ID;}
            set {_ID=value;}
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