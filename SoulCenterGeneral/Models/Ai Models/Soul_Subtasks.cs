using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace SoulCenterProject
{
    #region Soul_Subtasks
    public class Soul_Subtasks
    {
        #region Member Variables
        protected int _SubtaskID;
        protected int _TaskID;
        protected string _SubtaskDescription;
        protected string _Status;
        protected int _SortOrder;
        protected DateTime _StartDate;
        protected DateTime _DueDate;
        protected int _AssignedTo;
        #endregion
        #region Constructors
        public Soul_Subtasks() { }
        public Soul_Subtasks(int TaskID, string SubtaskDescription, string Status, int SortOrder, DateTime StartDate, DateTime DueDate, int AssignedTo)
        {
            this._TaskID=TaskID;
            this._SubtaskDescription=SubtaskDescription;
            this._Status=Status;
            this._SortOrder=SortOrder;
            this._StartDate=StartDate;
            this._DueDate=DueDate;
            this._AssignedTo=AssignedTo;
        }
        #endregion
        #region Public Properties
        public virtual int SubtaskID
        {
            get {return _SubtaskID;}
            set {_SubtaskID=value;}
        }
        public virtual int TaskID
        {
            get {return _TaskID;}
            set {_TaskID=value;}
        }
        public virtual string SubtaskDescription
        {
            get {return _SubtaskDescription;}
            set {_SubtaskDescription=value;}
        }
        public virtual string Status
        {
            get {return _Status;}
            set {_Status=value;}
        }
        public virtual int SortOrder
        {
            get {return _SortOrder;}
            set {_SortOrder=value;}
        }
        public virtual DateTime StartDate
        {
            get {return _StartDate;}
            set {_StartDate=value;}
        }
        public virtual DateTime DueDate
        {
            get {return _DueDate;}
            set {_DueDate=value;}
        }
        public virtual int AssignedTo
        {
            get {return _AssignedTo;}
            set {_AssignedTo=value;}
        }
        #endregion
    }
    #endregion
}