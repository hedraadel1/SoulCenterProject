namespace SoulCenterProject.SoulControls
{
    partial class SoulCodeIDE
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
            Syncfusion.Windows.Forms.Edit.Implementation.Config.Config config1 = new Syncfusion.Windows.Forms.Edit.Implementation.Config.Config();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoulCodeIDE));
            this.radDock1 = new Telerik.WinControls.UI.Docking.RadDock();
            this.documentWindow1 = new Telerik.WinControls.UI.Docking.DocumentWindow();
            this.syntaxEditor = new Syncfusion.Windows.Forms.Edit.EditControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FontSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.radDropDownButton1 = new Telerik.WinControls.UI.RadDropDownButton();
            this.radMenuHeaderItem1 = new Telerik.WinControls.UI.RadMenuHeaderItem();
            this.radMenuSeparatorItem1 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radMenuButtonItem_Copy = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.radMenuButtonItem_GoToEnd = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.radMenuSeparatorItem2 = new Telerik.WinControls.UI.RadMenuSeparatorItem();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.documentContainer1 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip1 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.radMenuButtonItem_GoToStart = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.TextBox_TaskName = new System.Windows.Forms.TextBox();
            this.TextBox_Langname = new System.Windows.Forms.TextBox();
            this.TextBox_CodeFor = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).BeginInit();
            this.radDock1.SuspendLayout();
            this.documentWindow1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.syntaxEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).BeginInit();
            this.documentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).BeginInit();
            this.documentTabStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radDock1
            // 
            this.radDock1.ActiveWindow = this.documentWindow1;
            this.radDock1.Controls.Add(this.documentContainer1);
            this.radDock1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDock1.IsCleanUpTarget = true;
            this.radDock1.Location = new System.Drawing.Point(0, 0);
            this.radDock1.MainDocumentContainer = this.documentContainer1;
            this.radDock1.Name = "radDock1";
            this.radDock1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // 
            // 
            this.radDock1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radDock1.Size = new System.Drawing.Size(1349, 667);
            this.radDock1.SplitterWidth = 10;
            this.radDock1.TabIndex = 0;
            this.radDock1.TabStop = false;
            this.radDock1.ThemeName = "Aqua";
            // 
            // documentWindow1
            // 
            this.documentWindow1.Controls.Add(this.syntaxEditor);
            this.documentWindow1.Controls.Add(this.groupControl1);
            this.documentWindow1.Controls.Add(this.tableLayoutPanel2);
            this.documentWindow1.Controls.Add(this.radDropDownButton1);
            this.documentWindow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentWindow1.Location = new System.Drawing.Point(4, 31);
            this.documentWindow1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.documentWindow1.Name = "documentWindow1";
            this.documentWindow1.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.TabbedDocument;
            this.documentWindow1.Size = new System.Drawing.Size(1341, 632);
            this.documentWindow1.Text = "documentWindow1";
            // 
            // syntaxEditor
            // 
            this.syntaxEditor.AllowDrop = true;
            this.syntaxEditor.AllowZoom = true;
            this.syntaxEditor.AutoCompleteSingleLexem = true;
            this.syntaxEditor.AutoIndentMode = Syncfusion.Windows.Forms.Edit.Enums.AutoIndentMode.Smart;
            this.syntaxEditor.AutoScroll = true;
            this.syntaxEditor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.syntaxEditor.ChangedLinesMarkingLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(98)))));
            this.syntaxEditor.CodeSnipptSize = new System.Drawing.Size(100, 100);
            this.syntaxEditor.Configurator = config1;
            this.syntaxEditor.ContextChoiceBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.syntaxEditor.ContextChoiceBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(166)))), ((int)(((byte)(50)))));
            this.syntaxEditor.ContextChoiceForeColor = System.Drawing.SystemColors.InfoText;
            this.syntaxEditor.ContextPromptBackgroundBrush = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))));
            this.syntaxEditor.ContextTooltipBackgroundBrush = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(236))))));
            this.syntaxEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxEditor.EnableBlockSelection = true;
            this.syntaxEditor.EnableSmartInBlockIndent = true;
            this.syntaxEditor.GraphicsCompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            this.syntaxEditor.GraphicsInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.syntaxEditor.GraphicsSmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            this.syntaxEditor.HighlightCurrentLine = true;
            this.syntaxEditor.IndicatorMarginBackColor = System.Drawing.Color.Empty;
            this.syntaxEditor.LineNumbersColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.syntaxEditor.Location = new System.Drawing.Point(0, 84);
            this.syntaxEditor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.syntaxEditor.MarkerAreaWidth = 20;
            this.syntaxEditor.Name = "syntaxEditor";
            this.syntaxEditor.ReadOnly = true;
            this.syntaxEditor.RenderRightToLeft = false;
            this.syntaxEditor.SaveOnClose = false;
            this.syntaxEditor.ScrollPosition = new System.Drawing.Point(0, 218);
            this.syntaxEditor.SelectionMarginForegroundColor = System.Drawing.Color.RosyBrown;
            this.syntaxEditor.SelectionTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(214)))), ((int)(((byte)(255)))));
            this.syntaxEditor.SetMarkerOnEncodingChanged = true;
            this.syntaxEditor.ShowEndOfLine = false;
            this.syntaxEditor.ShowIndentationBlockBorders = true;
            this.syntaxEditor.Size = new System.Drawing.Size(1341, 515);
            this.syntaxEditor.StatusBarSettings.CoordsPanel.Width = 150;
            this.syntaxEditor.StatusBarSettings.EncodingPanel.Width = 100;
            this.syntaxEditor.StatusBarSettings.FileNamePanel.AutoSize = true;
            this.syntaxEditor.StatusBarSettings.FileNamePanel.Width = 77;
            this.syntaxEditor.StatusBarSettings.InsertPanel.Width = 33;
            this.syntaxEditor.StatusBarSettings.Offcie2007ColorScheme = Syncfusion.Windows.Forms.Office2007Theme.Blue;
            this.syntaxEditor.StatusBarSettings.Offcie2010ColorScheme = Syncfusion.Windows.Forms.Office2010Theme.Blue;
            this.syntaxEditor.StatusBarSettings.StatusPanel.Width = 70;
            this.syntaxEditor.StatusBarSettings.TextPanel.Width = 80;
            this.syntaxEditor.StatusBarSettings.VisualStyle = Syncfusion.Windows.Forms.Tools.Controls.StatusBar.VisualStyle.Default;
            this.syntaxEditor.TabIndex = 7;
            this.syntaxEditor.Text = resources.GetString("syntaxEditor.Text");
            this.syntaxEditor.TextAreaWidth = 800;
            this.syntaxEditor.UseXPStyleBorder = true;
            this.syntaxEditor.VisualColumn = 1;
            this.syntaxEditor.VScrollMode = Syncfusion.Windows.Forms.Edit.ScrollMode.Immediate;
            this.syntaxEditor.WordWrap = true;
            this.syntaxEditor.WordWrapColumnMeasuringFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxEditor.ZoomFactor = 1F;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tableLayoutPanel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControl1.Location = new System.Drawing.Point(0, 599);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1341, 33);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Tools";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.74453F));
            this.tableLayoutPanel1.Controls.Add(this.FontSizeTrackBar, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1337, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FontSizeTrackBar
            // 
            this.FontSizeTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FontSizeTrackBar.Location = new System.Drawing.Point(4, 2);
            this.FontSizeTrackBar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.FontSizeTrackBar.Maximum = 30;
            this.FontSizeTrackBar.Minimum = 6;
            this.FontSizeTrackBar.Name = "FontSizeTrackBar";
            this.FontSizeTrackBar.Size = new System.Drawing.Size(1329, 25);
            this.FontSizeTrackBar.TabIndex = 2;
            this.FontSizeTrackBar.Value = 6;
            this.FontSizeTrackBar.Scroll += new System.EventHandler(this.FontSizeTrackBar_Scroll);
            // 
            // radDropDownButton1
            // 
            this.radDropDownButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radDropDownButton1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDropDownButton1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuHeaderItem1,
            this.radMenuSeparatorItem1,
            this.radMenuButtonItem_Copy,
            this.radMenuButtonItem_GoToStart,
            this.radMenuButtonItem_GoToEnd,
            this.radMenuSeparatorItem2,
            this.radMenuItem1});
            this.radDropDownButton1.Location = new System.Drawing.Point(0, 0);
            this.radDropDownButton1.Name = "radDropDownButton1";
            // 
            // 
            // 
            this.radDropDownButton1.RootElement.CustomFontStyle = System.Drawing.FontStyle.Bold;
            this.radDropDownButton1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radDropDownButton1.Size = new System.Drawing.Size(1341, 46);
            this.radDropDownButton1.TabIndex = 8;
            this.radDropDownButton1.Text = "Options";
            this.radDropDownButton1.ThemeName = "Aqua";
            // 
            // radMenuHeaderItem1
            // 
            this.radMenuHeaderItem1.Name = "radMenuHeaderItem1";
            this.radMenuHeaderItem1.Text = "radMenuHeaderItem1";
            this.radMenuHeaderItem1.UseCompatibleTextRendering = false;
            // 
            // radMenuSeparatorItem1
            // 
            this.radMenuSeparatorItem1.Name = "radMenuSeparatorItem1";
            this.radMenuSeparatorItem1.Text = "radMenuSeparatorItem1";
            this.radMenuSeparatorItem1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radMenuSeparatorItem1.UseCompatibleTextRendering = false;
            // 
            // radMenuButtonItem_Copy
            // 
            this.radMenuButtonItem_Copy.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radMenuButtonItem_Copy.Margin = new System.Windows.Forms.Padding(0, 11, 0, 11);
            this.radMenuButtonItem_Copy.MinSize = new System.Drawing.Size(0, 33);
            this.radMenuButtonItem_Copy.Name = "radMenuButtonItem_Copy";
            this.radMenuButtonItem_Copy.Text = "Copy";
            this.radMenuButtonItem_Copy.UseCompatibleTextRendering = false;
            // 
            // radMenuButtonItem_GoToEnd
            // 
            this.radMenuButtonItem_GoToEnd.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold);
            this.radMenuButtonItem_GoToEnd.Name = "radMenuButtonItem_GoToEnd";
            this.radMenuButtonItem_GoToEnd.Text = "Go To End";
            this.radMenuButtonItem_GoToEnd.UseCompatibleTextRendering = false;
            // 
            // radMenuSeparatorItem2
            // 
            this.radMenuSeparatorItem2.Name = "radMenuSeparatorItem2";
            this.radMenuSeparatorItem2.Text = "radMenuSeparatorItem2";
            this.radMenuSeparatorItem2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.radMenuSeparatorItem2.UseCompatibleTextRendering = false;
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "radMenuItem1";
            this.radMenuItem1.UseCompatibleTextRendering = false;
            // 
            // documentContainer1
            // 
            this.documentContainer1.Controls.Add(this.documentTabStrip1);
            this.documentContainer1.Name = "documentContainer1";
            this.documentContainer1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // 
            // 
            this.documentContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.documentContainer1.SizeInfo.AbsoluteSize = new System.Drawing.Size(368, 200);
            this.documentContainer1.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainer1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-59, 0);
            this.documentContainer1.SplitterWidth = 10;
            this.documentContainer1.ThemeName = "Aqua";
            // 
            // documentTabStrip1
            // 
            this.documentTabStrip1.Controls.Add(this.documentWindow1);
            this.documentTabStrip1.Location = new System.Drawing.Point(0, 0);
            this.documentTabStrip1.Name = "documentTabStrip1";
            // 
            // 
            // 
            this.documentTabStrip1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.documentTabStrip1.SelectedIndex = 0;
            this.documentTabStrip1.Size = new System.Drawing.Size(1349, 667);
            this.documentTabStrip1.TabIndex = 2;
            this.documentTabStrip1.TabStop = false;
            this.documentTabStrip1.ThemeName = "Aqua";
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.IsCleanUpTarget = true;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(427, 706);
            this.radSplitContainer1.SplitterWidth = 10;
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.ThemeName = "Aqua";
            // 
            // radMenuButtonItem_GoToStart
            // 
            this.radMenuButtonItem_GoToStart.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold);
            this.radMenuButtonItem_GoToStart.Name = "radMenuButtonItem_GoToStart";
            this.radMenuButtonItem_GoToStart.Text = "Go To Start";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.TextBox_CodeFor, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.TextBox_Langname, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.TextBox_TaskName, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 46);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1341, 38);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // TextBox_TaskName
            // 
            this.TextBox_TaskName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_TaskName.Location = new System.Drawing.Point(897, 3);
            this.TextBox_TaskName.Multiline = true;
            this.TextBox_TaskName.Name = "TextBox_TaskName";
            this.TextBox_TaskName.Size = new System.Drawing.Size(441, 32);
            this.TextBox_TaskName.TabIndex = 0;
            // 
            // TextBox_Langname
            // 
            this.TextBox_Langname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Langname.Location = new System.Drawing.Point(3, 3);
            this.TextBox_Langname.Multiline = true;
            this.TextBox_Langname.Name = "TextBox_Langname";
            this.TextBox_Langname.Size = new System.Drawing.Size(441, 32);
            this.TextBox_Langname.TabIndex = 2;
            // 
            // TextBox_CodeFor
            // 
            this.TextBox_CodeFor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_CodeFor.Location = new System.Drawing.Point(450, 3);
            this.TextBox_CodeFor.Multiline = true;
            this.TextBox_CodeFor.Name = "TextBox_CodeFor";
            this.TextBox_CodeFor.Size = new System.Drawing.Size(441, 32);
            this.TextBox_CodeFor.TabIndex = 3;
            // 
            // SoulCodeIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radDock1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SoulCodeIDE";
            this.Size = new System.Drawing.Size(1349, 667);
            this.Load += new System.EventHandler(this.SoulCodeIDE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).EndInit();
            this.radDock1.ResumeLayout(false);
            this.documentWindow1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.syntaxEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FontSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).EndInit();
            this.documentContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).EndInit();
            this.documentTabStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.Docking.RadDock radDock1;
        private Telerik.WinControls.UI.Docking.DocumentWindow documentWindow1;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer1;
        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.Docking.DocumentTabStrip documentTabStrip1;
        private Telerik.WinControls.UI.RadDropDownButton radDropDownButton1;
        private Telerik.WinControls.UI.RadMenuHeaderItem radMenuHeaderItem1;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem1;
        private Telerik.WinControls.UI.RadMenuButtonItem radMenuButtonItem_Copy;
        private Telerik.WinControls.UI.RadMenuButtonItem radMenuButtonItem_GoToEnd;
        private Telerik.WinControls.UI.RadMenuSeparatorItem radMenuSeparatorItem2;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar FontSizeTrackBar;
        public Syncfusion.Windows.Forms.Edit.EditControl syntaxEditor;
        private Telerik.WinControls.UI.RadMenuButtonItem radMenuButtonItem_GoToStart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox TextBox_Langname;
        private System.Windows.Forms.TextBox TextBox_TaskName;
        private System.Windows.Forms.TextBox TextBox_CodeFor;
    }
}
