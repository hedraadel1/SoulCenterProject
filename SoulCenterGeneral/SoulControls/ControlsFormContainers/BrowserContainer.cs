using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using SoulCenterProject.Helpers.SplashAndWaitForms;
using SoulCenterProject.Modules.MainCenter;

namespace SoulCenterProject.SoulControls.ControlsFormContainers
{
    public partial class BrowserContainer : Form
    {
               private SoulBrowser soulstudioinstance = new SoulBrowser();
        public BrowserContainer()
        {
            InitializeComponent();
        }

        private void BrowserContainer_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm2));

            //  SplashScreenManager1.SetWaitFormCaption("Soul Studio Is Loading");
            SplashScreenManager.Default.SetWaitFormDescription("Soul Studio Is Loading" + "%");

            soulstudioinstance.Dock = DockStyle.Fill;
            panel1.Controls.Add(soulstudioinstance);
            panel1.BringToFront();

            SplashScreenManager.CloseForm();
        }
    }
}
