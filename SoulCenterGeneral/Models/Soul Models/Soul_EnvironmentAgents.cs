/// <summary>
/// Represents a relationship between an environment and an agent in the Soul application.
/// </summary>
namespace SoulCenterProject.Models.Soul_Models
{
    /// <summary>
    /// Represents a relationship between an environment and an agent in the Soul application.
    /// </summary>
    public class Soul_EnvironmentAgents
    {
        #region Member Variables
        protected int _EnvironmentID;
        protected int _AgentID;
        #endregion
        #region Constructors

        public Soul_EnvironmentAgents()
        {
        }
        #endregion
        #region Public Properties
        public virtual int EnvironmentID
        {
            get { return _EnvironmentID; }
            set { _EnvironmentID = value; }
        }
        public virtual int AgentID
        {
            get { return _AgentID; }
            set { _AgentID = value; }
        }
        #endregion
    }
}