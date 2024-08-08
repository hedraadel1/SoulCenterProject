//-----------------------------------------------------------------------
// <copyright file="PromptSavedTemplate.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace SoulCenterProject.Models.Ai_Models
{
    public class PromptSavedTemplate
    {
        public int CategoryID { get; set; }

        public List<SoulPromptComponent> Components { get; set; }  // Navigation property for components

        public string Description { get; set; }

        public string Name { get; set; }

        public int SavedTemplateID { get; set; }
    }
}
