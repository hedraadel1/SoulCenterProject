using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SoulCenterProject.SoulControls.ControlsFormContainers
{
    public partial class MessageContainerForTest : Form
    {

            private System.Timers.Timer delayTimer;
        private List<MessagesContainer> createdMessageContainers = new List<MessagesContainer>();
        bool isdev = false;
        public MessageContainerForTest()
        {
            InitializeComponent(); 
        }

        private void delayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            delayTimer.Stop();
            foreach (var messageContainer in createdMessageContainers)
            {
                messageContainer.MessageOutput = "This message is added after 10 seconds";
            }
            createdMessageContainers.Clear(); // Clear the list after processing

        }
        private void CreateAndAddSeparator(bool isdevexpress)
        {
            var separatorControl = new DevExpress.XtraEditors.SeparatorControl
            {

                BackColor = Color.Transparent,
                Dock = DockStyle.Top,
                LineAlignment = DevExpress.XtraEditors.Alignment.Center,
                LineColor = Color.White,
                LineStyle = System.Drawing.Drawing2D.DashStyle.Custom,
                LineThickness = 2,
                Location = new Point(33, 245),
                LookAndFeel = { UseDefaultLookAndFeel = false, SkinMaskColor = Color.Black, SkinMaskColor2 = Color.Black },
                Name = "separatorControl4",
                Padding = new Padding(9, 3, 9, 3),
                Size = new Size(644, 23),
                TabIndex = 26
            };

            if (isdevexpress)
            {

                Scrollbar_Devexpress.Controls.Add(separatorControl);
            }
            else
            {
                 Scrollbar_Telerik.Controls.Add(separatorControl);
            }
            
        }
        public MessagesContainer CreateNewMessageAddToPanel(bool isdevexpress)
        {
            MessagesContainer ms = new MessagesContainer();
            ms.Dock = DockStyle.Top;

            if (isdevexpress)
            {
                Scrollbar_Devexpress.Controls.Add(ms);
                CreateAndAddSeparator(isdevexpress);
            }
            else
            {
                Scrollbar_Telerik.Controls.Add(ms);
                CreateAndAddSeparator(isdevexpress);
            }

            createdMessageContainers.Add(ms); // Add to the list
            return ms;
        }
        public void AddMessageToPanel(string message,bool isdevexpress)
        {
            CreateNewMessageAddToPanel(isdevexpress);
       
        }
        private void simpleButton_Devexpress_Click(object sender, EventArgs e)
        {
            CreateNewMessageAddToPanel(true); // Add message to Devexpress  panel

            delayTimer = new System.Timers.Timer(5000); // 10 seconds delay
            delayTimer.Elapsed += new ElapsedEventHandler(delayTimer_Elapsed);
            createdMessageContainers.Clear(); // Clear the list before starting timer
            delayTimer.Start();

        }

        private void simpleButton_Telerik_Click(object sender, EventArgs e)
        {
            CreateNewMessageAddToPanel(false); // Add message to Telerik panel

            delayTimer = new System.Timers.Timer(10000); // 10 seconds delay
            delayTimer.Elapsed += new ElapsedEventHandler(delayTimer_Elapsed);
            delayTimer.Start();
          
        }
    }
}
