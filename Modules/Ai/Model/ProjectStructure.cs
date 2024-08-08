//-----------------------------------------------------------------------
// <copyright file="ProjectStructure.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace SoulCenterProject.Modules.Ai.Model
{
    public class ProjectStructure
    {
        public string Name { get; set; }

        public List<NamespaceStructure> Namespaces { get; set; }
    }
}
