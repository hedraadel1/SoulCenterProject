using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

using Timer = System.Windows.Forms.Timer;

using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;

using System.Drawing;
using System.ComponentModel;

using Telerik.WinForms.Documents.Model;
using Telerik.WinForms.Documents.Proofing;
using Telerik.WinControls.RichTextEditor.UI;
using Color = System.Drawing.Color;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;

namespace SoulCenterProject.SoulControls
{
    public partial class MessagesContainer : UserControl
    {
        private string _MessageOutput;

        private int originalHeight;

        private Timer timer;
        private DateTime startTime;
        private TimeSpan elapsedTime;
        private string _SeachText;
        private Font currentFont;

        public string MessageOutput
        {
            get { return _MessageOutput; }
            set
            {
                _MessageOutput = value;
                MessageText.Text = value;
            }
        }
        public DateTime StartTimeValue
        {
            get { return startTime; }
            set
            {
                startTime = value;
            }
        }
        public MessagesContainer()
        {
            InitializeComponent();


            // dockPanel1_messagecontainer.ShowCaption = false;
            timer = new Timer();
            timer.Interval = 1000; // Set timer interval to 1 second
            timer.Tick += new EventHandler(timer_Tick);
            originalHeight = dockPanel1_messagecontainer.Height;
        }

        public bool IsTimerRunning { get; private set; }

        public void StartTimer()
        {
            if (!IsTimerRunning)
            {
                startTime = DateTime.Now;
                elapsedTime = TimeSpan.Zero;
                timer.Start();
                updateStartTimeLabel();
                IsTimerRunning = true;
            }
        }

        public void StopTimer()
        {
            if (IsTimerRunning)
            {
                timer.Stop();
                updateElapsedTimeLabel();
                IsTimerRunning = false;
            }
        }

        private void updateStartTimeLabel()
        {
            barButtonItem_Starttime.Caption = startTime.ToString("hh:mm:ss");
        }

        private void updateElapsedTimeLabel()
        {
            elapsedTime = DateTime.Now - startTime;
            barButtonItem_duration.Caption = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            updateElapsedTimeLabel();
        }

        public void StartNewMessage(MessageRole msgrole, string name)
        {
           // MessageText.Font = FontStyle.Bold;
           
         //   MessageText.ChangeTextAlignment(Telerik.WinForms.Documents.Layout.RadTextAlignment.Center);

            barEditItem4.Visibility = BarItemVisibility.Always;
            StartTimer();
            if (msgrole == MessageRole.Model)
            {
                barHeaderItem_Sender.Caption = "Name : " + name + "| Role :" + MessageRole.Model.ToString();
                MessageText.BackColor = System.Drawing.Color.White;
                MessageText.ForeColor = Color.Black;
            }

            if (msgrole == MessageRole.System)
            {
                barHeaderItem_Sender.Caption = "Name : " + "System" + "| Role :" + MessageRole.System.ToString();
                MessageText.BackColor = Color.FromArgb(232, 142, 23);
                MessageText.ForeColor = Color.White; 
            }

            if (msgrole == MessageRole.User)
            {
                barHeaderItem_Sender.Caption = "Name : " + name + "| Role :" + MessageRole.User.ToString();
                MessageText.BackColor = Color.Wheat;
                MessageText.ForeColor = Color.Black;

            }

            if (msgrole == MessageRole.Function)
            {
                barHeaderItem_Sender.Caption = "Name : " + "Function" + "| Role :" + MessageRole.Function.ToString();
                MessageText.BackColor = Color.FromArgb(232, 142, 23);
                MessageText.ForeColor = Color.Black;
            }

            if (msgrole == MessageRole.Other)
            {
                barHeaderItem_Sender.Caption = "Name : " + name + "| Role :" + MessageRole.Other.ToString();
                MessageText.BackColor = Color.FromArgb(232, 142, 23);
                MessageText.ForeColor = Color.Black;
            }
        }

        public void MessageReceived()
        {
            barEditItem4.Visibility = BarItemVisibility.Never;
            StopTimer();
        }

        public enum MessageRole
        {
            Model,
            User,
            Function,
            System,
            Other
        }

        private void MessageText_Click(object sender, EventArgs e)
        {
        }

        private void barButtonItem_SmallerHeight_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Height = 55;
        }

        private void barButtonItem_BiggerHeight_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Height = 555;
        }

        private void barButtonItem_RightToLeft_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageText.Select(0, MessageText.Text.Length);
            MessageText.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void barButtonItem_TextToMiddle_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageText.Select(0, MessageText.Text.Length);
            MessageText.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void barButtonItem_LeftToRight_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageText.Select(0, MessageText.Text.Length);
            MessageText.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void barButtonItem_ReGenerate_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barButtonItem_ValidateResposense_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barCheckItem_Delete_CheckedChanged(object sender, ItemClickEventArgs e)
        {
        }

        private void barButtonItem_Text_Smaller_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageText.Font != null && MessageText.Font.Size > 6)
            {
                float newSize = MessageText.Font.Size - 1;

                currentFont = new Font(currentFont.FontFamily, newSize);
                MessageText.Font = currentFont;
            }
        }

        private void barButtonItem_TextBigger_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentFont = MessageText.Font;
            if (MessageText.Font != null)
            {
                float newSize = MessageText.Font.Size + 1;

                currentFont = new Font(currentFont.FontFamily, newSize);
                MessageText.Font = currentFont;
            }
        }

        private void barButtonItem_edit_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barButtonItem_copy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageText.SelectedText != string.Empty)
            {
                MessageText.Copy();
            }
        }

        private void barButtonItem_OriginalHeight_ItemClick(object sender, ItemClickEventArgs e)
        {
            dockPanel1_messagecontainer.Height = originalHeight;
        }

        private void MessagesContainer_Load(object sender, EventArgs e)
        {
           
        }

        private void barButtonItem_CopyAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageText.SelectedText != string.Empty)
            {
                MessageText.Copy();
            }
        }

        private void barButtonItem_SelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageText.Select(MessageText.Text.Length, 0);
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
             
        }

        private void barEditItem8_EditValueChanged(object sender, EventArgs e)
        {
            SelectAllMatches(barEditItem9.EditValue.ToString());
        }

        private void CreateCustomDictionary()
        {
        
        }

        private void MessagesContainer_Resize(object sender, EventArgs e)
        {
            int x = this.Height;
            barEditItem_messagecontainerheight.EditValue = x;
        }

        private void barEditItem_messagecontainerheight_EditValueChanged(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(barEditItem_messagecontainerheight.EditValue) ;
            this.Height = x;
        }
    }
}