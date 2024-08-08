using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_PromptComponentPositions
    public class Soul_PromptComponentPositions
    {
        #region Member Variables
        protected int _PositionID;
        protected int _ComponentTypeID;
        protected int _Position;
        protected string _Constraints;
        #endregion
        #region Constructors
        public Soul_PromptComponentPositions() { }
        public Soul_PromptComponentPositions(int ComponentTypeID, int Position, string Constraints)
        {
            this._ComponentTypeID=ComponentTypeID;
            this._Position=Position;
            this._Constraints=Constraints;
        }
        #endregion
        #region Public Properties
        public virtual int PositionID
        {
            get {return _PositionID;}
            set {_PositionID=value;}
        }
        public virtual int ComponentTypeID
        {
            get {return _ComponentTypeID;}
            set {_ComponentTypeID=value;}
        }
        public virtual int Position
        {
            get {return _Position;}
            set {_Position=value;}
        }
        public virtual string Constraints
        {
            get {return _Constraints;}
            set {_Constraints=value;}
        }
        #endregion
    }
    #endregion
}