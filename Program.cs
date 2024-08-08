//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Windows.Forms;
using SoulCenterProject.Modules.MainCenter;

namespace SoulCenterProject
{
    using SoulCenterProject.Modules.Ai.Views;
    using SoulCenterProject.Modules.ProjectManagment.Views;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SoulStudioContainer());
            //  Application.Run(new MessageContainerForTest());
        }
    }
}
