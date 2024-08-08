//-----------------------------------------------------------------------
// <copyright file="DirectoryInfo.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using FileInfo = SoulCenterProject.SoulControls.ControlsModels.FileInfo;

namespace SoulCenterProject.SoulControls.ControlsHelpers.CodeEditor
{
    public class DirectoryInfo : FileInfo
    {
        public DirectoryInfo(string fullPath, string name) : base(fullPath, name)
        { Children = new ObservableCollection<FileInfo>(); }

        public void LoadChildren()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(FullPath);
                foreach(string directory in dirs)
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directory);
                    Children.Add(new DirectoryInfo(directory, directoryInfo.Name));
                }

                string[] files = Directory.GetFiles(FullPath);
                foreach(string file in files)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    Children.Add(new FileInfo(file, fileInfo.Name));
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<FileInfo> Children { get; private set; }
    }
}
