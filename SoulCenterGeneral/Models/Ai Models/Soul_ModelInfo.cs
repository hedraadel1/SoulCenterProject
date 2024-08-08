using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_ModelInfo
    public class Soul_ModelInfo
    {
        #region Member Variables
        protected string _Name;
        protected string _BaseModelId;
        protected string _Version;
        protected string _DisplayName;
        protected string _Description;
        protected int _InputTokenLimit;
        protected int _OutputTokenLimit;
        protected string _SupportedGenerationMethods;
        protected unknown _Temperature;
        protected unknown _TopP;
        protected int _TopK;
        #endregion
        #region Constructors
        public Soul_ModelInfo() { }
        public Soul_ModelInfo(string DisplayName, string Description, int InputTokenLimit, int OutputTokenLimit, string SupportedGenerationMethods, unknown Temperature, unknown TopP, int TopK)
        {
            this._DisplayName=DisplayName;
            this._Description=Description;
            this._InputTokenLimit=InputTokenLimit;
            this._OutputTokenLimit=OutputTokenLimit;
            this._SupportedGenerationMethods=SupportedGenerationMethods;
            this._Temperature=Temperature;
            this._TopP=TopP;
            this._TopK=TopK;
        }
        #endregion
        #region Public Properties
        public virtual string Name
        {
            get {return _Name;}
            set {_Name=value;}
        }
        public virtual string BaseModelId
        {
            get {return _BaseModelId;}
            set {_BaseModelId=value;}
        }
        public virtual string Version
        {
            get {return _Version;}
            set {_Version=value;}
        }
        public virtual string DisplayName
        {
            get {return _DisplayName;}
            set {_DisplayName=value;}
        }
        public virtual string Description
        {
            get {return _Description;}
            set {_Description=value;}
        }
        public virtual int InputTokenLimit
        {
            get {return _InputTokenLimit;}
            set {_InputTokenLimit=value;}
        }
        public virtual int OutputTokenLimit
        {
            get {return _OutputTokenLimit;}
            set {_OutputTokenLimit=value;}
        }
        public virtual string SupportedGenerationMethods
        {
            get {return _SupportedGenerationMethods;}
            set {_SupportedGenerationMethods=value;}
        }
        public virtual unknown Temperature
        {
            get {return _Temperature;}
            set {_Temperature=value;}
        }
        public virtual unknown TopP
        {
            get {return _TopP;}
            set {_TopP=value;}
        }
        public virtual int TopK
        {
            get {return _TopK;}
            set {_TopK=value;}
        }
        #endregion
    }
    #endregion
}