namespace SoulCenterProject.Models.Soul_Models
{
    #region Soul_PromptTemplates
    public class Soul_PromptTemplates
    {
        #region Member Variables
        protected int _TemplateID;
        protected string _TemplateName;
        protected string _TemplateDescription;
        #endregion
        #region Constructors
        public Soul_PromptTemplates() { }
        public Soul_PromptTemplates(string TemplateName, string TemplateDescription)
        {
            this._TemplateName=TemplateName;
            this._TemplateDescription=TemplateDescription;
        }
        #endregion
        #region Public Properties
        public virtual int TemplateID
        {
            get {return _TemplateID;}
            set {_TemplateID=value;}
        }
        public virtual string TemplateName
        {
            get {return _TemplateName;}
            set {_TemplateName=value;}
        }
        public virtual string TemplateDescription
        {
            get {return _TemplateDescription;}
            set {_TemplateDescription=value;}
        }
        #endregion
    }
    #endregion
}