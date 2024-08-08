namespace SoulCenterProject.SoulControls.Utiles
{
    partial class SoulFileExploerer
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
            DevExpress.XtraDialogs.NavigationBinding navigationBinding1 = new DevExpress.XtraDialogs.NavigationBinding();
            DevExpress.XtraDialogs.NavigationBinding navigationBinding2 = new DevExpress.XtraDialogs.NavigationBinding();
            DevExpress.XtraDialogs.NavigationBinding navigationBinding3 = new DevExpress.XtraDialogs.NavigationBinding();
            DevExpress.XtraDialogs.NavigationBinding navigationBinding4 = new DevExpress.XtraDialogs.NavigationBinding();
            DevExpress.XtraDialogs.NavigationBinding navigationBinding5 = new DevExpress.XtraDialogs.NavigationBinding();
            DevExpress.XtraDialogs.FileExplorerExtensions.EnvironmentSpecialFolderNode environmentSpecialFolderNode1 = new DevExpress.XtraDialogs.FileExplorerExtensions.EnvironmentSpecialFolderNode();
            this.fileExplorerAssistant1 = new DevExpress.XtraDialogs.FileExplorerAssistant(this.components);
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.breadCrumbEdit1 = new DevExpress.XtraEditors.BreadCrumbEdit();
            this.breadCrumbExtension1 = new DevExpress.XtraDialogs.FileExplorerExtensions.BreadCrumbExtension();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListExtension1 = new DevExpress.XtraDialogs.FileExplorerExtensions.TreeListExtension();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlExtension1 = new DevExpress.XtraDialogs.FileExplorerExtensions.GridControlExtension();
            this.sidePanel3 = new DevExpress.XtraEditors.SidePanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.previewPanelExtension1 = new DevExpress.XtraDialogs.FileExplorerExtensions.PreviewPanelExtension();
            ((System.ComponentModel.ISupportInitialize)(this.fileExplorerAssistant1)).BeginInit();
            this.sidePanel1.SuspendLayout();
            this.sidePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.breadCrumbEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.breadCrumbExtension1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListExtension1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlExtension1)).BeginInit();
            this.sidePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewPanelExtension1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileExplorerAssistant1
            // 
            this.fileExplorerAssistant1.Extensions.Add(this.breadCrumbExtension1);
            this.fileExplorerAssistant1.Extensions.Add(this.treeListExtension1);
            this.fileExplorerAssistant1.Extensions.Add(this.gridControlExtension1);
            this.fileExplorerAssistant1.Extensions.Add(this.previewPanelExtension1);
            this.fileExplorerAssistant1.Form = this;
            navigationBinding1.Source = this.treeListExtension1;
            navigationBinding1.Target = this.gridControlExtension1;
            navigationBinding2.Source = this.gridControlExtension1;
            navigationBinding2.Target = this.treeListExtension1;
            navigationBinding3.Source = this.breadCrumbExtension1;
            navigationBinding3.Target = this.gridControlExtension1;
            navigationBinding4.Source = this.gridControlExtension1;
            navigationBinding4.Target = this.breadCrumbExtension1;
            navigationBinding5.Source = this.gridControlExtension1;
            navigationBinding5.Target = this.previewPanelExtension1;
            this.fileExplorerAssistant1.NavigationBindings.Add(navigationBinding1);
            this.fileExplorerAssistant1.NavigationBindings.Add(navigationBinding2);
            this.fileExplorerAssistant1.NavigationBindings.Add(navigationBinding3);
            this.fileExplorerAssistant1.NavigationBindings.Add(navigationBinding4);
            this.fileExplorerAssistant1.NavigationBindings.Add(navigationBinding5);
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.treeList1);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel1.Location = new System.Drawing.Point(0, 24);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(250, 728);
            this.sidePanel1.TabIndex = 0;
            // 
            // sidePanel2
            // 
            this.sidePanel2.AllowResize = false;
            this.sidePanel2.Controls.Add(this.breadCrumbEdit1);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel2.Location = new System.Drawing.Point(0, 0);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.sidePanel2.Size = new System.Drawing.Size(1087, 24);
            this.sidePanel2.TabIndex = 1;
            // 
            // breadCrumbEdit1
            // 
            this.breadCrumbEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breadCrumbEdit1.Location = new System.Drawing.Point(0, 3);
            this.breadCrumbEdit1.Name = "breadCrumbEdit1";
            this.breadCrumbEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.breadCrumbEdit1.Size = new System.Drawing.Size(1087, 18);
            this.breadCrumbEdit1.TabIndex = 0;
            // 
            // breadCrumbExtension1
            // 
            this.breadCrumbExtension1.Control = this.breadCrumbEdit1;
            this.breadCrumbExtension1.CurrentPath = "C:\\Users\\Onoo\\Documents";
            // 
            // treeList1
            // 
            this.treeList1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(249, 728);
            this.treeList1.TabIndex = 0;
            // 
            // treeListExtension1
            // 
            this.treeListExtension1.Control = this.treeList1;
            this.treeListExtension1.CurrentPath = "C:\\Users\\Onoo\\Documents";
            this.treeListExtension1.IconSize = new System.Drawing.Size(16, 16);
            this.treeListExtension1.RootNodes.Add(environmentSpecialFolderNode1);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(250, 24);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(587, 728);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // gridControlExtension1
            // 
            this.gridControlExtension1.Control = this.gridControl1;
            this.gridControlExtension1.CurrentPath = "C:\\Users\\Onoo\\Documents";
            this.gridControlExtension1.IconSize = new System.Drawing.Size(16, 16);
            // 
            // sidePanel3
            // 
            this.sidePanel3.Controls.Add(this.panelControl1);
            this.sidePanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidePanel3.Location = new System.Drawing.Point(837, 24);
            this.sidePanel3.Name = "sidePanel3";
            this.sidePanel3.Size = new System.Drawing.Size(250, 728);
            this.sidePanel3.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(1, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(249, 728);
            this.panelControl1.TabIndex = 0;
            // 
            // previewPanelExtension1
            // 
            this.previewPanelExtension1.Control = this.panelControl1;
            // 
            // SoulFileExploerer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.sidePanel3);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.sidePanel2);
            this.Name = "SoulFileExploerer";
            this.Size = new System.Drawing.Size(1087, 752);
            ((System.ComponentModel.ISupportInitialize)(this.fileExplorerAssistant1)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            this.sidePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.breadCrumbEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.breadCrumbExtension1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListExtension1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlExtension1)).EndInit();
            this.sidePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewPanelExtension1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDialogs.FileExplorerAssistant fileExplorerAssistant1;
        private DevExpress.XtraDialogs.FileExplorerExtensions.BreadCrumbExtension breadCrumbExtension1;
        private DevExpress.XtraEditors.BreadCrumbEdit breadCrumbEdit1;
        private DevExpress.XtraDialogs.FileExplorerExtensions.TreeListExtension treeListExtension1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraDialogs.FileExplorerExtensions.GridControlExtension gridControlExtension1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraDialogs.FileExplorerExtensions.PreviewPanelExtension previewPanelExtension1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SidePanel sidePanel3;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
    }
}
