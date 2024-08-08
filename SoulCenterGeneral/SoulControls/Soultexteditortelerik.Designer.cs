namespace SoulCenterProject.SoulControls
{
    partial class Soultexteditortelerik
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Soultexteditortelerik));
            this.radRichTextEditor1 = new Telerik.WinControls.UI.RadRichTextEditor();
            this.richTextEditorRibbonBar1 = new Telerik.WinControls.UI.RichTextEditorRibbonBar();
            this.radialMenu1 = new DevExpress.XtraBars.Ribbon.RadialMenu(this.components);
            this.barCheckItem_Header = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem_Copy = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem_Paste = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.RadialMenuEasyTemplates = new DevExpress.XtraBars.BarLinkContainerItem();
            this.EasyTemplate_oneline = new DevExpress.XtraBars.BarButtonItem();
            this.EasyTemplate_Twoline = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.radRichTextEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richTextEditorRibbonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // radRichTextEditor1
            // 
            this.radRichTextEditor1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.radRichTextEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radRichTextEditor1.DocumentInheritsDefaultStyleSettings = true;
            this.radRichTextEditor1.IsAdvancedSelectionEnabled = true;
            this.radRichTextEditor1.IsSpellCheckingEnabled = true;
            this.radRichTextEditor1.LayoutMode = Telerik.WinForms.Documents.Model.DocumentLayoutMode.Flow;
            this.radRichTextEditor1.LineBreakingRuleLanguage = Telerik.WinForms.Documents.Model.LineBreakingRuleLanguage.ChineseSimplified;
            this.radRichTextEditor1.Location = new System.Drawing.Point(0, 166);
            this.radRichTextEditor1.Name = "radRichTextEditor1";
            this.barManager1.SetPopupContextMenu(this.radRichTextEditor1, this.radialMenu1);
            this.radRichTextEditor1.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(179)))), ((int)(((byte)(236)))), ((int)(((byte)(248)))));
            this.radRichTextEditor1.SelectionStroke = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(236)))), ((int)(((byte)(248)))));
            this.radRichTextEditor1.ShowComments = true;
            this.radRichTextEditor1.ShowMergeFieldsHighlight = true;
            this.radRichTextEditor1.Size = new System.Drawing.Size(704, 99);
            this.radRichTextEditor1.TabIndex = 1;
            this.radRichTextEditor1.TextChanged += new System.EventHandler(this.radRichTextEditor1_TextChanged);
            // 
            // richTextEditorRibbonBar1
            // 
            this.richTextEditorRibbonBar1.ApplicationMenuStyle = Telerik.WinControls.UI.ApplicationMenuStyle.BackstageView;
            this.richTextEditorRibbonBar1.AssociatedRichTextEditor = this.radRichTextEditor1;
            this.richTextEditorRibbonBar1.BuiltInStylesVersion = Telerik.WinForms.Documents.Model.Styles.BuiltInStylesVersion.Office2013;
            this.richTextEditorRibbonBar1.EnableKeyMap = false;
            this.richTextEditorRibbonBar1.Location = new System.Drawing.Point(0, 0);
            this.richTextEditorRibbonBar1.Name = "richTextEditorRibbonBar1";
            this.richTextEditorRibbonBar1.ShowLayoutModeButton = true;
            this.richTextEditorRibbonBar1.Size = new System.Drawing.Size(704, 166);
            this.richTextEditorRibbonBar1.TabIndex = 2;
            this.richTextEditorRibbonBar1.TabStop = false;
            this.richTextEditorRibbonBar1.Visible = false;
            // 
            // radialMenu1
            // 
            this.radialMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_Header),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_Copy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_Paste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.RadialMenuEasyTemplates)});
            this.radialMenu1.Manager = this.barManager1;
            this.radialMenu1.Name = "radialMenu1";
            // 
            // barCheckItem_Header
            // 
            this.barCheckItem_Header.Caption = "Input Header";
            this.barCheckItem_Header.Id = 2;
            this.barCheckItem_Header.Name = "barCheckItem_Header";
            this.barCheckItem_Header.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_Header_ItemClick);
            // 
            // barCheckItem_Copy
            // 
            this.barCheckItem_Copy.Caption = "Copy";
            this.barCheckItem_Copy.Id = 0;
            this.barCheckItem_Copy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barCheckItem_Copy.ImageOptions.Image")));
            this.barCheckItem_Copy.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barCheckItem_Copy.ImageOptions.LargeImage")));
            this.barCheckItem_Copy.Name = "barCheckItem_Copy";
            this.barCheckItem_Copy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_Copy_ItemClick);
            // 
            // barCheckItem_Paste
            // 
            this.barCheckItem_Paste.Caption = "Paste";
            this.barCheckItem_Paste.Id = 3;
            this.barCheckItem_Paste.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barCheckItem_Paste.ImageOptions.Image")));
            this.barCheckItem_Paste.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barCheckItem_Paste.ImageOptions.LargeImage")));
            this.barCheckItem_Paste.Name = "barCheckItem_Paste";
            this.barCheckItem_Paste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem_Paste_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 1;
            this.barSubItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.Image")));
            this.barSubItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.LargeImage")));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_Header),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_Paste)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // RadialMenuEasyTemplates
            // 
            this.RadialMenuEasyTemplates.Caption = "Easy Templates";
            this.RadialMenuEasyTemplates.Id = 4;
            this.RadialMenuEasyTemplates.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.EasyTemplate_oneline),
            new DevExpress.XtraBars.LinkPersistInfo(this.EasyTemplate_Twoline)});
            this.RadialMenuEasyTemplates.Name = "RadialMenuEasyTemplates";
            // 
            // EasyTemplate_oneline
            // 
            this.EasyTemplate_oneline.Caption = "------";
            this.EasyTemplate_oneline.Id = 5;
            this.EasyTemplate_oneline.Name = "EasyTemplate_oneline";
            this.EasyTemplate_oneline.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.EasyTemplate_oneline_ItemClick);
            // 
            // EasyTemplate_Twoline
            // 
            this.EasyTemplate_Twoline.Caption = "======";
            this.EasyTemplate_Twoline.Id = 6;
            this.EasyTemplate_Twoline.Name = "EasyTemplate_Twoline";
            this.EasyTemplate_Twoline.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.EasyTemplate_Twoline_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barCheckItem_Copy,
            this.barSubItem1,
            this.barCheckItem_Header,
            this.barCheckItem_Paste,
            this.RadialMenuEasyTemplates,
            this.EasyTemplate_oneline,
            this.EasyTemplate_Twoline});
            this.barManager1.MaxItemId = 7;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(704, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 265);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(704, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 265);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(704, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 265);
            // 
            // Soultexteditortelerik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radRichTextEditor1);
            this.Controls.Add(this.richTextEditorRibbonBar1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Soultexteditortelerik";
            this.Size = new System.Drawing.Size(704, 265);
            ((System.ComponentModel.ISupportInitialize)(this.radRichTextEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richTextEditorRibbonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Telerik.WinControls.UI.RadRichTextEditor radRichTextEditor1;
        public Telerik.WinControls.UI.RichTextEditorRibbonBar richTextEditorRibbonBar1;
        private DevExpress.XtraBars.Ribbon.RadialMenu radialMenu1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem_Header;
        private DevExpress.XtraBars.BarButtonItem barCheckItem_Copy;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barCheckItem_Paste;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLinkContainerItem RadialMenuEasyTemplates;
        private DevExpress.XtraBars.BarButtonItem EasyTemplate_oneline;
        private DevExpress.XtraBars.BarButtonItem EasyTemplate_Twoline;
    }
}
