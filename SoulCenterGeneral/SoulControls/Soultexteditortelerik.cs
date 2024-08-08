//-----------------------------------------------------------------------
// <copyright file="Soultexteditortelerik.cs" company="Onoo">
//   Author: Eng Hedra Adel
//   Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.Model;

namespace SoulCenterProject.SoulControls
{
    public partial class Soultexteditortelerik : UserControl
    {
        private string _PromptInputText;
        private string _SeachText;

        public void InsertSeparator(string separatorType)
        {
            string separatorText;

            switch (separatorType)
            {
                case "eh1":
                    separatorText = "--------------------";
                    break;
                case "eh2":
                    separatorText = "==========";
                    break;


                // ... (أضف أنواع تانية من الفواصل حسب احتياجاتك) 
                default:
                    separatorText = "---";
                    break;
            }


            // إدراج الفاصل في الـ RichTextEditor
            radRichTextEditor1.Insert(separatorText);
        }

        public Soultexteditortelerik()
        {
            InitializeComponent();
        }

        /// <summary>
        ///set the Search input text.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Search input text.")]
        public string Searchin
        {
            get { return _SeachText; }
            set
            {
                _SeachText = value;
                SelectAllMatches(value);
            }
        }

        private void SelectAllMatches(string toSearch)
        {
            this.radRichTextEditor1.Document.Selection.Clear(); // this clears the selection before processing
            Telerik.WinForms.Documents.TextSearch.DocumentTextSearch search =
                new Telerik.WinForms.Documents.TextSearch.DocumentTextSearch(this.radRichTextEditor1.Document);
            foreach (var textRange in search.FindAll(toSearch))
            {
                this.radRichTextEditor1.Document.Selection.AddSelectionStart(textRange.StartPosition);
                this.radRichTextEditor1.Document.Selection.AddSelectionEnd(textRange.EndPosition);
            }


            this.radRichTextEditor1.ChangeTextHighlightColor(Telerik.WinControls.RichTextEditor.UI.Colors.LightGray);


            // will highlight all selected words in LightGray
            this.radRichTextEditor1.ChangeFontSize(Unit.PointToDip(32));


            // will increase the font size of the words to 30 DIP
            this.radRichTextEditor1.ChangeFontFamily(
                new Telerik.WinControls.RichTextEditor.UI.FontFamily("Comic Sans MS"));


            // will change the font family of the spans, containing these words.
        }

        /// <summary>
        /// Gets or sets the prompt input text.
        /// </summary>
        [Browsable(true)]
        [Category("Data")]
        [Description("Prompt input text.")]
        public string PromptInput
        {
            get { return _PromptInputText; }
            set
            {
                _PromptInputText = value;
                richTextEditorRibbonBar1.Text = value;
            }
        }

        private void barCheckItem_Header_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barCheckItem_Header.Checked)
            {
                richTextEditorRibbonBar1.Visible = true;
            }
            else
            {
                richTextEditorRibbonBar1.Visible = false;
            }
        }

        private void barCheckItem_Copy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radRichTextEditor1.Copy();
        }

        private void barCheckItem_Paste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            radRichTextEditor1.Paste();
        }

        private void EasyTemplate_oneline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSeparator("eh1");
        }

        private void EasyTemplate_Twoline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSeparator("eh2");
        }

        private void radRichTextEditor1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}