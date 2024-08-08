//-----------------------------------------------------------------------
// <copyright file="SoulCenter.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using DevExpress.Sparkline;

using SoulCenterProject.Helpers.Utils;
using SoulCenterProject.Modules.ProjectManagment.Views;

namespace SoulCenterProject.Modules.MainCenter
{
    /// <inheritdoc/>
    [ClassInfo("View")]
    public partial class SoulCenter : DevExpress.XtraEditors.XtraForm
    {

        private bool _continueUpdating = true;
        //ChatUI chatu = new ChatUI();
        private bool shouldexit = false;

        public SoulCenter()
        {
            InitializeComponent();
            //soulstudio so = new soulstudio();
            //so.Dock = DockStyle.Fill;
            //SoulPanel.Controls.Add(so);
            //Thread hardwareThread = new Thread(hardwareInfo);
            //hardwareThread.Start();
        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement32_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement33_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
        }


        // @@num:1

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barEditItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barEditItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void Button_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shouldexit = true;
            Application.Exit();
        }

        private void Button_Maximize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Button_Minimize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TopMost = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            WindowState = FormWindowState.Minimized;
        }

        private void CreateSparkline()
        {
            // Create an Area view and assign it to the sparkline.
            LineSparklineView view = new LineSparklineView();


            // Customize area appearance.
            view.Color = Color.Aqua;
            view.LineWidth = 10;


            // Show markers.
            view.HighlightStartPoint = true;
            view.HighlightEndPoint = true;
            view.HighlightMaxPoint = true;
            view.HighlightMinPoint = true;
            view.HighlightNegativePoints = true;
            view.SetSizeForAllMarkers(10);
        }

        private void dockPanel2_Click(object sender, EventArgs e)
        {
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void GroupControl3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void hardwareInfo()
        {

            //while (true) // Assuming you want continuous monitoring
            //{
            //    PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //    PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            //    cpuCounter.NextValue();
            //    ramCounter.NextValue();
            //    Thread.Sleep(100); // Get accurate readings

            //    float cpuUsage = cpuCounter.NextValue();
            //    float availableRam = ramCounter.NextValue();

            //    float totalRam = 16 * 1024; // Replace 16 with your actual total RAM (GB)
            //    float ramUsagePercentage = ((totalRam - availableRam) / totalRam) * 100;


            //    // Update the UI (thread-safe way)
            //    if (shouldexit)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        Invoke(
            //            (MethodInvoker)delegate
            //            {
            //                Gauge_Cpu.BeginUpdate();
            //                Gauge_Cpu.Value = (Int32)cpuUsage;
            //                Gauge_Cpu.EndUpdate();
            //                Gauge_Cpu_percentage.Text = cpuUsage.ToString();

            //                Gauge_Ram_percentage.Text = ramUsagePercentage.ToString();

            //                FormProgress.EditValue = (Int32)cpuUsage;

            //                if (IsInternetConnected())
            //                {
            //                    digitalGauge4.Text = "on";
            //                    InternetStatussign(true);
            //                }
            //                else
            //                {
            //                    digitalGauge4.Text = "off";
            //                    InternetStatussign(false);
            //                }

            //                UpdateNetworkStats();
            //            });
            //    }

            //    Thread.Sleep(1000); // Update interval (adjust as needed)
            //}
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        private void InternetStatussign(bool IsNetworkOnline)
        {
            //if (IsNetworkOnline)
            //{
            //    if (stateIndicatorComponent1.StateIndex == 2)
            //    {
            //        stateIndicatorComponent1.StateIndex = 3;
            //    }
            //    else
            //    {
            //        stateIndicatorComponent1.StateIndex = 2;
            //    }
            //}
            //else
            //{
            //    if (stateIndicatorComponent1.StateIndex == 0)
            //    {
            //        stateIndicatorComponent1.StateIndex = 1;
            //    }
            //    else
            //    {
            //        stateIndicatorComponent1.StateIndex = 0;
            //        digitalGauge2.Text = "000";
            //        digitalGauge3.Text = "000";
            //    }
            //}
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            //chatu.radSyntaxEditor1.Text = "";
            //chatu.Show();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
        }

        private void SoulCenter_Load(object sender, EventArgs e)
        {
            //CreateSparkline();
            //Gauge_Cpu.EnableAnimation = true;
            //Gauge_Cpu.EasingMode = EasingMode.EaseOut;
            //Gauge_Cpu.EasingFunction = new BackEase();


            //timer1.Start();
        }

        private void stepProgressBar1_Click(object sender, EventArgs e)
        {
        }

        private void stepProgressBar1_SelectedItemChanged(
            object sender,
            DevExpress.XtraEditors.StepProgressBarSelectedItemChangedEventArgs e)
        {
            //            if (stepProgressBar1.SelectedItemIndex == 0)
            //            {
            //                ComponentStep_Text.Text = @"It will Build The Windows Form UI";
            //            }
            //            if (stepProgressBar1.SelectedItemIndex == 1)
            //            {
            //                ComponentStep_Text.Text = @"It Consist from 
            //-- The Job : it will display some tasks and need two textbox with name's xxx,yyy and one button with name ggg
            //--- The Names for every Element like textbox's,buttons,etc..
            //-- The Files and the job,template for every file
            //-- The Design : how it will be , and from where will get it 
            //-- The agents that will excute every component part , and every agent will has it's instructions ,role,promt 
            //-- The agent that will be as the manager ";
            //            }
            //            if (stepProgressBar1.SelectedItemIndex == 2)
            //            {
            //                ComponentStep_Text.Text = @"last ? 

            //Steps With Example :-";
            //            }
        }

        private void stepProgressBarItem1_ProgressChanged(object sender, EventArgs e)
        {
        }

        private void tabNavigationPage1_Paint(object sender, PaintEventArgs e)
        {
        }

        void Timer_Tick(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Thread hardwareThread = new Thread(hardwareinto);
            //hardwareThread.Start();
            //LinearScaleProvider linearScaleComponent1 = gauge.Scale as LinearScaleProvider;

            //stateIndicatorComponent1.Shader = BaseColorShader.Empty;
        }

        private void Toggle_Topmost_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Toggle_Topmost.Checked)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void toolboxItem1_ItemChanged(object sender, DevExpress.XtraToolbox.ToolboxItemChangedEventArgs e)
        {
        }

        public static bool IsInternetConnected()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }


        // Network Performance Monitoring
        public void UpdateNetworkStats()
        {
            //if (!NetworkInterface.GetIsNetworkAvailable())
            //{
            //    return;
            //}

            //NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            //foreach (NetworkInterface ni in interfaces)
            //{
            //    if (ni.OperationalStatus == OperationalStatus.Up)
            //    {
            //        IPv4InterfaceStatistics stats = ni.GetIPv4Statistics();

            //        long downloadSpeed = stats.BytesReceived / 1024; // In KB/s
            //        long uploadSpeed = stats.BytesSent / 1024; // In KB/s


            //        // UI Update (Thread-safe way)
            //        Invoke(
            //            (MethodInvoker)delegate
            //            {
            //                digitalGauge2.Text = downloadSpeed.ToString();
            //                digitalGauge3.Text = uploadSpeed.ToString();


            //                // Update other UI elements as needed   
            //                InternetStatussign(true);
            //            });

            //        if (IsInternetConnected())
            //        {
            //            InternetStatussign(true);
            //        }
            //        else
            //        {
            //            InternetStatussign(false);
            //        }

            //        break; // Assuming you want stats for the first active interface
            //    }
            //}
        }

        private void BarButton_SoulStudio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SoulStudioContainer stfrm = new SoulStudioContainer();
            stfrm.Show();
        }

        private void BarButton_FunctionBuilder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FunctionBuilder fbfrm = new FunctionBuilder();
            fbfrm.Show();
        }

        private void BarButtonItem_Minimize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void BarButtonItem_Exit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }
    }
}