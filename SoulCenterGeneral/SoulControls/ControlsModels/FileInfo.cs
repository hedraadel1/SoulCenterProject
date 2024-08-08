//-----------------------------------------------------------------------
// <copyright file="FileInfo.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SoulCenterProject.SoulControls.ControlsModels
{
    public class FileInfo
    {
        public FileInfo(string fullPath, string name)
        {
            FullPath = fullPath;
            Name = name;
        }

        public string FullPath { get; set; }

        public string Name { get; set; }
    }
}
