using System;

namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_Tasks
    public class Soul_Tasks
    {
        #region Member Variables
        protected int _TaskID;
        protected string _TaskDescription;
        protected string _Status;
        protected string _Priority;
        protected DateTime _DueDate;
        protected DateTime _StartDate;
        protected int _AssignedTo;
        #endregion
        #region Constructors
        public Soul_Tasks() { }
        public Soul_Tasks(string TaskDescription, string Status, string Priority, DateTime DueDate, DateTime StartDate, int AssignedTo)
        {
            this._TaskDescription=TaskDescription;
            this._Status=Status;
            this._Priority=Priority;
            this._DueDate=DueDate;
            this._StartDate=StartDate;
            this._AssignedTo=AssignedTo;
        }
        #endregion
        #region Public Properties
        public virtual int TaskID
        {
            get {return _TaskID;}
            set {_TaskID=value;}
        }
        public virtual string TaskDescription
        {
            get {return _TaskDescription;}
            set {_TaskDescription=value;}
        }
        public virtual string Status
        {
            get {return _Status;}
            set {_Status=value;}
        }
        public virtual string Priority
        {
            get {return _Priority;}
            set {_Priority=value;}
        }
        public virtual DateTime DueDate
        {
            get {return _DueDate;}
            set {_DueDate=value;}
        }
        public virtual DateTime StartDate
        {
            get {return _StartDate;}
            set {_StartDate=value;}
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