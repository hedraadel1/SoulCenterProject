//-----------------------------------------------------------------------
// <copyright file="soulstudio.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;

public class LoginUserControl : XtraUserControl
{
    public LoginUserControl()
    {
        LayoutControl lc = new LayoutControl();
        lc.Dock = DockStyle.Fill;
        TextEdit teLogin = new TextEdit();
        TextEdit tePassword = new TextEdit();
        CheckEdit ceKeep = new CheckEdit() { Text = "Keep me signed in" };
        lc.AddItem(String.Empty, teLogin).TextVisible = false;
        lc.AddItem(String.Empty, tePassword).TextVisible = false;
        lc.AddItem(String.Empty, ceKeep);
        this.Controls.Add(lc);
        this.Dock = DockStyle.Fill;
    }
}