namespace SoulCenterProject.SoulControls.ControlsFormContainers
{
    partial class MessageContainerForTest
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Scrollbar_Devexpress = new DevExpress.XtraEditors.XtraScrollableControl();
            this.vScrollBar1 = new DevExpress.XtraEditors.VScrollBar();
            this.Scrollbar_Telerik = new Telerik.WinControls.UI.RadVScrollBar();
            this.simpleButton_Devexpress = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Telerik = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Scrollbar_Telerik)).BeginInit();
            this.SuspendLayout();
            // 
            // Scrollbar_Devexpress
            // 
            this.Scrollbar_Devexpress.Dock = System.Windows.Forms.DockStyle.Top;
            this.Scrollbar_Devexpress.Location = new System.Drawing.Point(0, 0);
            this.Scrollbar_Devexpress.Name = "Scrollbar_Devexpress";
            this.Scrollbar_Devexpress.Size = new System.Drawing.Size(942, 254);
            this.Scrollbar_Devexpress.TabIndex = 0;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(900, 573);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(42, 105);
            this.vScrollBar1.TabIndex = 1;
            // 
            // Scrollbar_Telerik
            // 
            this.Scrollbar_Telerik.Dock = System.Windows.Forms.DockStyle.Top;
            this.Scrollbar_Telerik.Location = new System.Drawing.Point(0, 254);
            this.Scrollbar_Telerik.Name = "Scrollbar_Telerik";
            this.Scrollbar_Telerik.Size = new System.Drawing.Size(942, 313);
            this.Scrollbar_Telerik.TabIndex = 2;
            this.Scrollbar_Telerik.ThemeName = "ControlDefault";
            // 
            // simpleButton_Devexpress
            // 
            this.simpleButton_Devexpress.Location = new System.Drawing.Point(23, 605);
            this.simpleButton_Devexpress.Name = "simpleButton_Devexpress";
            this.simpleButton_Devexpress.Size = new System.Drawing.Size(211, 71);
            this.simpleButton_Devexpress.TabIndex = 3;
            this.simpleButton_Devexpress.Text = "Devexpress";
            this.simpleButton_Devexpress.Click += new System.EventHandler(this.simpleButton_Devexpress_Click);
            // 
            // simpleButton_Telerik
            // 
            this.simpleButton_Telerik.Location = new System.Drawing.Point(250, 605);
            this.simpleButton_Telerik.Name = "simpleButton_Telerik";
            this.simpleButton_Telerik.Size = new System.Drawing.Size(211, 71);
            this.simpleButton_Telerik.TabIndex = 4;
            this.simpleButton_Telerik.Text = "Telerik";
            this.simpleButton_Telerik.Click += new System.EventHandler(this.simpleButton_Telerik_Click);
            // 
            // MessageContainerForTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 688);
            this.Controls.Add(this.simpleButton_Telerik);
            this.Controls.Add(this.simpleButton_Devexpress);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.Scrollbar_Telerik);
            this.Controls.Add(this.Scrollbar_Devexpress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MessageContainerForTest";
            this.Text = "MessageContainerForTest";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Scrollbar_Telerik)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl Scrollbar_Devexpress;
        private DevExpress.XtraEditors.VScrollBar vScrollBar1;
        private Telerik.WinControls.UI.RadVScrollBar Scrollbar_Telerik;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Devexpress;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Telerik;
    }
}