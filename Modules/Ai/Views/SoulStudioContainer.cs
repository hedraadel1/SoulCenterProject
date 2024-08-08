using DevExpress.XtraEditors;

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
using DevExpress.XtraWaitForm;

using SoulCenterProject.Helpers.SplashAndWaitForms;
using SoulCenterProject.SoulControls.ComponentsTypes;

using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SoulCenterProject.Modules.MainCenter
{
    public partial class SoulStudioContainer : XtraForm
    {
        FlyoutAction addComponentAction = new FlyoutAction() { Caption = "Add new Component" };

        public soulstudio soulstudioinstance;

        public SoulStudioContainer()
        {
            InitializeComponent();


            //addComponentAction.Commands.Add(FlyoutCommand.Cancel);
            //addComponentAction.Commands.Add(new AddCommand());
            //userControlFlyout.Action = addComponentAction;
            soulstudioinstance = new soulstudio(this);


            //// Handling the QueryControl event that will populate all automatically generated Documents
            //this.windowsUIView1.QueryControl += windowsUIView1_QueryControl;
        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm2));


            //  SplashScreenManager1.SetWaitFormCaption("Soul Studio Is Loading");
            SplashScreenManager.Default.SetWaitFormDescription("Soul Studio Is Loading" + "%");


            //soulstudioinstance.Dock = DockStyle.Fill;
            //panel1.Controls.Add(soulstudioinstance);


            //panel1.BringToFront();

            //windowsUIButtonPanel1.SendToBack();
            SplashScreenManager.CloseForm();
        }

        private void SoulStudioContainer_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm2));

            SplashScreenManager.Default.SetWaitFormDescription("Soul Studio Is Loading" + "%");

            soulstudioinstance.Dock = DockStyle.Fill;
            panel1.Controls.Add(soulstudioinstance);
            //panel1.BringToFront();
            SplashScreenManager.CloseForm();
        }

        void windowsUIView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
        }

        private void tileContainer1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
        }
    }

    class AddCommand : FlyoutCommand
    {
        public override string Text
        {
            get { return "Add"; }
        }

        public override DialogResult Result
        {
            get { return DialogResult.OK; }
        }
    }
}