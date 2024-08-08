//-----------------------------------------------------------------------
// <copyright file="SoulCodeIDE.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using StyleCop;
using System;
using System.IO;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinForms.Controls.SyntaxEditor.Taggers;
using Telerik.WinForms.SyntaxEditor.Core.Tagging;
using Telerik.WinForms.SyntaxEditor.Core.Text;


namespace SoulCenterProject.SoulControls
{
    public partial class SoulCodeIDE : DevExpress.XtraEditors.XtraUserControl
    {
        public SoulCodeIDE()
        {
            InitializeComponent();

        }


        // Property to set content from a file path
        public string FilePath
        {
            set
            {
                syntaxEditor.InsertFile(value);
            }
        }

        public string Langname
        {
            get { return TextBox_Langname.Text; }
            set { TextBox_Langname.Text = value; }
        }

        public string TaskName
        {
            get { return TextBox_TaskName.Text; }
            set { TextBox_TaskName.Text = value; }
        }

        public string CodeFor
        {
            get { return TextBox_CodeFor.Text; }
            set { TextBox_CodeFor.Text = value; }
        }

        // Property to set content from a string
        public string CodeContent
        {
            get => syntaxEditor.Text;
            set => syntaxEditor.Text = value;
        }

        private void SoulCodeIDE_Load(object sender, System.EventArgs e)
        {

        }


        private void FontSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            syntaxEditor.ZoomFactor = FontSizeTrackBar.Value;
        }
    }
}

