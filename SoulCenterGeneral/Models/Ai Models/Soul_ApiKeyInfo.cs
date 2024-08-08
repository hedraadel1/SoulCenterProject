using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_ApiKeyInfo
    public class Soul_ApiKeyInfo
    {
        #region Member Variables
        protected string _Apikey;
        protected string _ApiKey_AccountName;
        protected string _ApiCompany;
        #endregion
        #region Constructors
        public Soul_ApiKeyInfo() { }
        public Soul_ApiKeyInfo(string ApiKey_AccountName, string ApiCompany)
        {
            this._ApiKey_AccountName=ApiKey_AccountName;
            this._ApiCompany=ApiCompany;
        }
        #endregion
        #region Public Properties
        public virtual string Apikey
        {
            get {return _Apikey;}
            set {_Apikey=value;}
        }
        public virtual string ApiKey_AccountName
        {
            get {return _ApiKey_AccountName;}
            set {_ApiKey_AccountName=value;}
        }
        public virtual string ApiCompany
        {
            get {return _ApiCompany;}
            set {_ApiCompany=value;}
        }
        #endregion
    }
    #endregion
}