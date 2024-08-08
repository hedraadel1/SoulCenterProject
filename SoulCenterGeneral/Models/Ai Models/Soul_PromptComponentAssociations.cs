using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_PromptComponentAssociations
    public class Soul_PromptComponentAssociations
    {
        #region Member Variables
        protected int _PromptID;
        protected int _ComponentID;
        protected int _TemplateID;
        #endregion
        #region Constructors
        public Soul_PromptComponentAssociations() { }
        public Soul_PromptComponentAssociations(int TemplateID)
        {
            this._TemplateID=TemplateID;
        }
        #endregion
        #region Public Properties
        public virtual int PromptID
        {
            get {return _PromptID;}
            set {_PromptID=value;}
        }
        public virtual int ComponentID
        {
            get {return _ComponentID;}
            set {_ComponentID=value;}
        }
        public virtual int TemplateID
        {
            get {return _TemplateID;}
            set {_TemplateID=value;}
        }
        #endregion
    }
    #endregion
}