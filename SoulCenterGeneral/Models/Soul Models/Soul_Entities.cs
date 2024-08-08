namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_Entities
    public class Soul_Entities
    {
        #region Member Variables
        protected int _EntityID;
        protected string _EntityType;
        protected string _EntityValue;
        protected double _ConfidenceScore; // Change unknown to double or float
        protected string _AdditionalInfo;
        #endregion
        #region Constructors
        public Soul_Entities() { }
        public Soul_Entities(string EntityType, string EntityValue, double ConfidenceScore, string AdditionalInfo)
        {
            this._EntityType = EntityType;
            this._EntityValue = EntityValue;
            this._ConfidenceScore = ConfidenceScore;
            this._AdditionalInfo = AdditionalInfo;
        }
        #endregion
        #region Public Properties
        public virtual int EntityID
        {
            get { return _EntityID; }
            set { _EntityID = value; }
        }
        public virtual string EntityType
        {
            get { return _EntityType; }
            set { _EntityType = value; }
        }
        public virtual string EntityValue
        {
            get { return _EntityValue; }
            set { _EntityValue = value; }
        }
        public virtual double ConfidenceScore
        {
            get { return _ConfidenceScore; }
            set { _ConfidenceScore = value; }
        }
        public virtual string AdditionalInfo
        {
            get { return _AdditionalInfo; }
            set { _AdditionalInfo = value; }
        }
        #endregion
    }
    #endregion
}