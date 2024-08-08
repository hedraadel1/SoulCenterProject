using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_EnvironmentAgents
    public class Soul_EnvironmentAgents
    {
        #region Member Variables
        protected int _EnvironmentID;
        protected int _AgentID;
        #endregion
        #region Constructors
        public Soul_EnvironmentAgents() { }
        public Soul_EnvironmentAgents()
        {
        }
        #endregion
        #region Public Properties
        public virtual int EnvironmentID
        {
            get {return _EnvironmentID;}
            set {_EnvironmentID=value;}
        }
        public virtual int AgentID
        {
            get {return _AgentID;}
            set {_AgentID=value;}
        }
        #endregion
    }
    #endregion
}