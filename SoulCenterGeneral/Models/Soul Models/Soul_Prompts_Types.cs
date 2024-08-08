namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_Prompts_Types
    public class Soul_Prompts_Types
    {
        #region Member Variables
        protected string _ComponentTypeName;
        protected int _ComponentTypeID;
        #endregion
        #region Constructors
        public Soul_Prompts_Types() { }
        public Soul_Prompts_Types(string ComponentTypeName)
        {
            this._ComponentTypeName=ComponentTypeName;
        }
        #endregion
        #region Public Properties
        public virtual string ComponentTypeName
        {
            get {return _ComponentTypeName;}
            set {_ComponentTypeName=value;}
        }
        public virtual int ComponentTypeID
        {
            get {return _ComponentTypeID;}
            set {_ComponentTypeID=value;}
        }
        #endregion
    }
    #endregion
}