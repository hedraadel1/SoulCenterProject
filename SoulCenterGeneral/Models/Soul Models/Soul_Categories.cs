namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_Categories
    public class Soul_Categories
    {
        #region Member Variables
        protected int _CategoryID;
        protected string _CategoryName;
        protected string _TableType;
        #endregion
        #region Constructors
        public Soul_Categories() { }
        public Soul_Categories(string CategoryName, string TableType)
        {
            this._CategoryName=CategoryName;
            this._TableType=TableType;
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
        public virtual string TableType
        {
            get {return _TableType;}
            set {_TableType=value;}
        }
        #endregion
    }
    #endregion
}