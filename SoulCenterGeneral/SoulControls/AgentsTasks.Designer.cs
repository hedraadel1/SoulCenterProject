namespace SoulCenterProject.SoulControls
{
    partial class AgentsTasks
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
            this.ListView_SubTasks = new DevExpress.XtraEditors.ListBoxControl();
            this.ListView_MainTasks = new DevExpress.XtraEditors.ListBoxControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.ListView_SubTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView_MainTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView_SubTasks
            // 
            this.ListView_SubTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListView_SubTasks.Location = new System.Drawing.Point(0, 336);
            this.ListView_SubTasks.Name = "ListView_SubTasks";
            this.ListView_SubTasks.Size = new System.Drawing.Size(460, 313);
            this.ListView_SubTasks.TabIndex = 0;
            // 
            // ListView_MainTasks
            // 
            this.ListView_MainTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.ListView_MainTasks.Location = new System.Drawing.Point(0, 0);
            this.ListView_MainTasks.Name = "ListView_MainTasks";
            this.ListView_MainTasks.Size = new System.Drawing.Size(460, 313);
            this.ListView_MainTasks.TabIndex = 1;
            this.ListView_MainTasks.SelectedValueChanged += new System.EventHandler(this.ListView_MainTasks_SelectedValueChanged);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.separatorControl1.Location = new System.Drawing.Point(0, 313);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(460, 23);
            this.separatorControl1.TabIndex = 2;
            // 
            // separatorControl2
            // 
            this.separatorControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.separatorControl2.Location = new System.Drawing.Point(0, 772);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(460, 19);
            this.separatorControl2.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 672);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(460, 100);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Commands";
            // 
            // separatorControl3
            // 
            this.separatorControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.separatorControl3.Location = new System.Drawing.Point(0, 649);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Size = new System.Drawing.Size(460, 23);
            this.separatorControl3.TabIndex = 5;
            // 
            // AgentsTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.separatorControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.separatorControl3);
            this.Controls.Add(this.ListView_SubTasks);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.ListView_MainTasks);
            this.Name = "AgentsTasks";
            this.Size = new System.Drawing.Size(460, 791);
            ((System.ComponentModel.ISupportInitialize)(this.ListView_SubTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListView_MainTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl ListView_SubTasks;
        private DevExpress.XtraEditors.ListBoxControl ListView_MainTasks;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
    }
}
