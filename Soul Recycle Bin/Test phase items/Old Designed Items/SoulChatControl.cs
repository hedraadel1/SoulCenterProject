//-----------------------------------------------------------------------
// <copyright file="SoulChatControl.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoulCenterProject.Helpers.Utils;
using SoulCenterProject.SoulControls;
using Telerik.WinControls.UI.Docking;

namespace SoulCenterProject.Test_phase_items.Old_Designed_Items
{
    /// <inheritdoc/>
    [ClassInfo("View")]

    public partial class SoulChatControl : UserControl
    {
        private SoultextEditor deveditor;
        private Soultexteditortelerik teleditor;

        public SoulChatControl()
        {
            InitializeComponent();
            deveditor = new SoultextEditor();
            teleditor = new Soultexteditortelerik();

            deveditor.Dock = DockStyle.Fill;
            teleditor.Dock = DockStyle.Fill;
            editorpanel.Controls.Add(deveditor);
        }

        private void AiModel_ExitAll_Click(object sender, EventArgs e) { Application.Exit(); }

        private void Barbutton_edtiortype_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(Barbutton_edtiortype.Checked == false)
            {
                editorpanel.Controls.Remove(teleditor);
                editorpanel.Controls.Add(deveditor);
            } else
            {
                editorpanel.Controls.Remove(deveditor);
                editorpanel.Controls.Add(teleditor);
            }
        }

        private void Button_ExitBarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { Application.Exit(); }

        private void Button_HideandDockAllInputs_ToggleStateChanged(
            object sender,
            Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if(Button_HideandDockAllInputs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                InputsContainer_ToolWindow_Main.DockState = DockState.Docked;
            } else
            {
                InputsContainer_ToolWindow_Main.DockState = DockState.AutoHide;
            }
        }

        private void Button_HideandDockHeader_ToggleStateChanged(
            object sender,
            Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if(Button_HideandDockHeader.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                Header_ToolWindow.DockState = DockState.Docked;
            } else
            {
                Header_ToolWindow.DockState = DockState.AutoHide;
            }
        }

        private void CodeInputEditor_DocumentContentChanged(
            object sender,
            Telerik.WinForms.SyntaxEditor.Core.Text.TextContentChangedEventArgs e)
        {
            digitalGauge1.Value = CodeInputEditor.Document.CurrentSnapshot.Length.ToString();
            int MaxInputvalue = Convert.ToInt32(digitalGauge1.Value);

            if(MaxInputvalue > 1000)
            {
                digitalGauge1.ForeColor = System.Drawing.Color.Red;
            } else
            {
                digitalGauge1.ForeColor = System.Drawing.Color.Chartreuse;
            }
        }

        private void commandBarDropDownButton1_Click(object sender, EventArgs e)
        {
        }

        private void digitalGauge1_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoadData()
        {
            // Create a DataTable
            DataTable table1 = new DataTable();

            // Add columns
            table1.Columns.Add("ID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Columns.Add("Selected", typeof(bool));

            // Add rows with data
            table1.Rows.Add(1, "Ivan Petrov", true);
            table1.Rows.Add(2, "Stefan Muler", true);
            table1.Rows.Add(3, "Alexandro Ricco", false);

            // Set the data source and display member for the RadMultiColumnComboBox
            radMultiColumnComboBox1.DataSource = table1;
            radMultiColumnComboBox1.DisplayMember = "Name";
        }

        private void MaxOutput_Trackbar_EditValueChanged(object sender, EventArgs e)
        {
            //digitalGauge1.Value = MaxOutput_Trackbar.Value.ToString();
        }

        private void pivotGridControl1_Click(object sender, EventArgs e)
        {
        }

        private void radMenuItem6_Click(object sender, EventArgs e)
        {
        }

        private void radMenuItem7_Click(object sender, EventArgs e)
        {
        }

        private void radRepeatButton1_Click(object sender, EventArgs e)
        {
        }

        private void radRepeatButton3_Click(object sender, EventArgs e)
        {
        }

        private void radRepeatButton6_Click(object sender, EventArgs e)
        { Header_ToolWindow.DockState = DockState.AutoHide; }

        private void radToggleButton1_ToggleStateChanged(
            object sender,
            Telerik.WinControls.UI.StateChangedEventArgs args)
        {
        }

        private void radToggleButton2_ToggleStateChanged(
            object sender,
            Telerik.WinControls.UI.StateChangedEventArgs args)
        {
        }

        private void radToggleButtonElement1_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                simpleButton1.DoDragDrop(sender as SimpleButton, DragDropEffects.All);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {
        }

        private void treeList1_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(typeof(SimpleButton)))
            {
                SimpleButton button = e.Data.GetData(typeof(SimpleButton)) as SimpleButton;
                treeList1.AppendNode(new object[] { button.Text }, -1);
            }
        }

        private void treeList1_DragOver(object sender, DragEventArgs e) { e.Effect = DragDropEffects.All; }


        public class MyDataItem
        {
            public string Column1 { get; set; }

            public string Column2 { get; set; }

            public string Column3 { get; set; }
        }
    }
}
