using System;
using System.Drawing;
using DevExpress.XtraSplashScreen;

namespace SoulCenterProject.Helpers.SplashAndWaitForms
{
    public partial class SoulStudioSplashScreen : SplashScreen
    {
        public SoulStudioSplashScreen()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © 1998-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void timer_animation_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();

            int one = rand.Next(0, 255);
            int two = rand.Next(0, 255);
            int three = rand.Next(0, 255);
            int four = rand.Next(0, 255);

            Label_Soul.ForeColor = Color.FromArgb(one, two, three, four);
        }

        private void SoulStudioSplashScreen_Load(object sender, EventArgs e)
        {
            timer_animation.Enabled = true;
            timer_animation.Start();
        }
    }
}