﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoulCenterProject.SoulControls.ComponentsTypes
{
    public partial class AddComponent : UserControl
    {
        public AddComponent()
        {
            InitializeComponent();


            // This line of code is generated by Data Source Configuration Wizard
            // Fill the SqlDataSource asynchronously
            //sqlDataSource2.FillAsync();
            // This line of code is generated by Data Source Configuration Wizard
            // Fill the SqlDataSource asynchronously
            sqlDataSource2.FillAsync();


            // This line of code is generated by Data Source Configuration Wizard
            // Fill the SqlDataSource asynchronously
            sqlDataSource2.FillAsync();
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void ComponentValueTextEdit_TextChanged(object sender, EventArgs e)
        {
            ComponentValueTextEdit11.Text = ComponentValueTextEdit.Text;
        }
    }
}