namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_MessageEntities
    public class Soul_MessageEntities
    {
        #region Member Variables
        protected int _MessageID;
        protected int _EntityID;
        protected double _Relevance; // Change unknown to double or float
        #endregion
        #region Constructors
        public Soul_MessageEntities() { }
        public Soul_MessageEntities(double Relevance)
        {
            this._Relevance = Relevance;
        }
        #endregion
        #region Public Properties
        public virtual int MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }
        public virtual int EntityID
        {
            get { return _EntityID; }
            set { _EntityID = value; }
        }
        public virtual double Relevance
        {
            get { return _Relevance; }
            set { _Relevance = value; }
        }
        #endregion
    }
    #endregion
}