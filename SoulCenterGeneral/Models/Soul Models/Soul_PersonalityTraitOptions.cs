namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_PersonalityTraitOptions
    public class Soul_PersonalityTraitOptions
    {
        #region Member Variables
        protected int _ID;
        protected string _TraitName;
        protected int _Soul_PersonalityTraitCategoryID;
        protected string _PersonalityTraitDocumentation;
        #endregion
        #region Constructors
        public Soul_PersonalityTraitOptions() { }
        public Soul_PersonalityTraitOptions(string TraitName, int Soul_PersonalityTraitCategoryID, string PersonalityTraitDocumentation)
        {
            this._TraitName=TraitName;
            this._Soul_PersonalityTraitCategoryID=Soul_PersonalityTraitCategoryID;
            this._PersonalityTraitDocumentation=PersonalityTraitDocumentation;
        }
        #endregion
        #region Public Properties
        public virtual int ID
        {
            get {return _ID;}
            set {_ID=value;}
        }
        public virtual string TraitName
        {
            get {return _TraitName;}
            set {_TraitName=value;}
        }
        public virtual int Soul_PersonalityTraitCategoryID
        {
            get {return _Soul_PersonalityTraitCategoryID;}
            set {_Soul_PersonalityTraitCategoryID=value;}
        }
        public virtual string PersonalityTraitDocumentation
        {
            get {return _PersonalityTraitDocumentation;}
            set {_PersonalityTraitDocumentation=value;}
        }
        #endregion
    }
    #endregion
}