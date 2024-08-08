//-----------------------------------------------------------------------
// <copyright file="ClassInfoAttribute.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace SoulCenterProject.Helpers.Utils
{
    public class ClassInfoAttribute : Attribute
    {
        public ClassInfoAttribute(string type, string[] properties = null, string[] methods = null)
        {
            Type = type;
            Properties = properties ?? Array.Empty<string>(); // Initialize to empty array
            Methods = methods ?? Array.Empty<string>(); // Initialize to empty array
        }

        public string[] Methods { get; set; } // List of class methods

        public string[] Properties { get; set; } // List of class properties

        public string Type { get; set; } // "Model", "View", etc.
    }
}
