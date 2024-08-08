//-----------------------------------------------------------------------
// <copyright file="SoulPromptComponent.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;

namespace SoulCenterProject.Models.Ai_Models
{
    public class SoulPromptComponent
    {
        public int CategoryID { get; set; }  // Assuming you use the foreign key approach

        public ComponentType ComponentType { get; set; }

        public string Custom1 { get; set; }

        public string Custom2 { get; set; }

        public string Custom3 { get; set; }

        public string Custom4 { get; set; }

        public string Custom5 { get; set; }

        public int ID { get; set; }

        public bool IsActive { get; set; }

        public bool IsEnabled { get; set; }

        public string Name { get; set; }

        public PromptSavedTemplate SavedTemplate { get; set; }  // Optional navigation property

        public string Value { get; set; }
    }

   
}
