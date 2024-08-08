using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_PromptComponents
    public class Soul_PromptComponents
    {
        #region Member Variables
        protected int _ComponentID;
        protected string _ComponentName;
        protected string _ComponentValue;
        protected int _ComponentTypeID;
        protected string _Custom;
        protected string _Custom;
        protected string _Custom;
        protected string _Custom;
        protected string _Custom;
        protected bool _isEnabled;
        protected bool _isActive;
        protected int _CategoryID;
        #endregion
        #region Constructors
        public Soul_PromptComponents() { }
        public Soul_PromptComponents(string ComponentName, string ComponentValue, int ComponentTypeID, string Custom, string Custom, string Custom, string Custom, string Custom, bool isEnabled, bool isActive, int CategoryID)
        {
            this._ComponentName=ComponentName;
            this._ComponentValue=ComponentValue;
            this._ComponentTypeID=ComponentTypeID;
            this._Custom=Custom;
            this._Custom=Custom;
            this._Custom=Custom;
            this._Custom=Custom;
            this._Custom=Custom;
            this._isEnabled=isEnabled;
            this._isActive=isActive;
            this._CategoryID=CategoryID;
        }
        #endregion
        #region Public Properties
        public virtual int ComponentID
        {
            get {return _ComponentID;}
            set {_ComponentID=value;}
        }
        public virtual string ComponentName
        {
            get {return _ComponentName;}
            set {_ComponentName=value;}
        }
        public virtual string ComponentValue
        {
            get {return _ComponentValue;}
            set {_ComponentValue=value;}
        }
        public virtual int ComponentTypeID
        {
            get {return _ComponentTypeID;}
            set {_ComponentTypeID=value;}
        }
        public virtual string Custom
        {
            get {return _Custom;}
            set {_Custom=value;}
        }
        public virtual string Custom
        {
            get {return _Custom;}
            set {_Custom=value;}
        }
        public virtual string Custom
        {
            get {return _Custom;}
            set {_Custom=value;}
        }
        public virtual string Custom
        {
            get {return _Custom;}
            set {_Custom=value;}
        }
        public virtual string Custom
        {
            get {return _Custom;}
            set {_Custom=value;}
        }
        public virtual bool IsEnabled
        {
            get {return _isEnabled;}
            set {_isEnabled=value;}
        }
        public virtual bool IsActive
        {
            get {return _isActive;}
            set {_isActive=value;}
        }
        public virtual int CategoryID
        {
            get {return _CategoryID;}
            set {_CategoryID=value;}
        }
        #endregion
    }
    #endregion
}