namespace SoulCenterProject.Modules.Ai.Views
{
    partial class Aichat
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
            this.Button_Close = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.RadChatPanel = new Telerik.WinControls.UI.RadChat();
            this.textBox_extract = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.Button_extract = new DevExpress.XtraEditors.SimpleButton();
            this.GenerateProjectStructure = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RadChatPanel)).BeginInit();
            this.RadChatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Close
            // 
            this.Button_Close.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Close.Appearance.ForeColor = System.Drawing.Color.Red;
            this.Button_Close.Appearance.Options.UseFont = true;
            this.Button_Close.Appearance.Options.UseForeColor = true;
            this.Button_Close.Dock = System.Windows.Forms.DockStyle.Left;
            this.Button_Close.Location = new System.Drawing.Point(2, 27);
            this.Button_Close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(252, 259);
            this.Button_Close.TabIndex = 7;
            this.Button_Close.Text = "اغلاق";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.RadChatPanel);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(1346, 382);
            this.groupControl2.TabIndex = 14;
            this.groupControl2.Text = "groupControl2";
            // 
            // RadChatPanel
            // 
            this.RadChatPanel.AutoAddUserMessages = false;
            this.RadChatPanel.AvatarSize = new System.Drawing.SizeF(106.8115F, 106.8115F);
            this.RadChatPanel.Controls.Add(this.textBox_extract);
            this.RadChatPanel.Controls.Add(this.textBox1);
            this.RadChatPanel.Controls.Add(this.progressBar1);
            this.RadChatPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RadChatPanel.Location = new System.Drawing.Point(2, -560);
            this.RadChatPanel.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.RadChatPanel.Name = "RadChatPanel";
            this.RadChatPanel.Size = new System.Drawing.Size(1342, 940);
            this.RadChatPanel.TabIndex = 3;
            this.RadChatPanel.Text = "radChat1";
            this.RadChatPanel.ThemeName = "Fluent";
            this.RadChatPanel.TimeSeparatorInterval = System.TimeSpan.Parse("1.00:00:00");
            this.RadChatPanel.SendMessage += new Telerik.WinControls.UI.SendMessageEventHandler(this.RadChatPanel_SendMessage);
            this.RadChatPanel.Click += new System.EventHandler(this.RadChatPanel_Click);
            // 
            // textBox_extract
            // 
            this.textBox_extract.Location = new System.Drawing.Point(775, 681);
            this.textBox_extract.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox_extract.Multiline = true;
            this.textBox_extract.Name = "textBox_extract";
            this.textBox_extract.Size = new System.Drawing.Size(390, 111);
            this.textBox_extract.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(676, 868);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(793, 23);
            this.textBox1.TabIndex = 9;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(676, 822);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(795, 36);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Visible = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.Button_extract);
            this.groupControl3.Controls.Add(this.GenerateProjectStructure);
            this.groupControl3.Controls.Add(this.Button_Close);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl3.Location = new System.Drawing.Point(0, 382);
            this.groupControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1346, 288);
            this.groupControl3.TabIndex = 15;
            this.groupControl3.Text = "groupControl3";
            // 
            // Button_extract
            // 
            this.Button_extract.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_extract.Appearance.ForeColor = System.Drawing.Color.Red;
            this.Button_extract.Appearance.Options.UseFont = true;
            this.Button_extract.Appearance.Options.UseForeColor = true;
            this.Button_extract.Dock = System.Windows.Forms.DockStyle.Left;
            this.Button_extract.Location = new System.Drawing.Point(745, 27);
            this.Button_extract.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Button_extract.Name = "Button_extract";
            this.Button_extract.Size = new System.Drawing.Size(1424, 259);
            this.Button_extract.TabIndex = 9;
            this.Button_extract.Text = "extract";
            this.Button_extract.Click += new System.EventHandler(this.Button_extract_Click);
            // 
            // GenerateProjectStructure
            // 
            this.GenerateProjectStructure.Appearance.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateProjectStructure.Appearance.ForeColor = System.Drawing.Color.Red;
            this.GenerateProjectStructure.Appearance.Options.UseFont = true;
            this.GenerateProjectStructure.Appearance.Options.UseForeColor = true;
            this.GenerateProjectStructure.Dock = System.Windows.Forms.DockStyle.Left;
            this.GenerateProjectStructure.Location = new System.Drawing.Point(254, 27);
            this.GenerateProjectStructure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GenerateProjectStructure.Name = "GenerateProjectStructure";
            this.GenerateProjectStructure.Size = new System.Drawing.Size(491, 259);
            this.GenerateProjectStructure.TabIndex = 8;
            this.GenerateProjectStructure.Text = "Get This Project Structure";
            this.GenerateProjectStructure.Click += new System.EventHandler(this.GenerateProjectStructure_Click);
            // 
            // Aichat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 670);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl3);
            this.Name = "Aichat";
            this.Text = "Aichat";
       
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RadChatPanel)).EndInit();
            this.RadChatPanel.ResumeLayout(false);
            this.RadChatPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton Button_Close;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private Telerik.WinControls.UI.RadChat RadChatPanel;
        private DevExpress.XtraEditors.SimpleButton GenerateProjectStructure;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox_extract;
        private DevExpress.XtraEditors.SimpleButton Button_extract;
    }
}