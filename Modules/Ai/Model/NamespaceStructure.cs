//-----------------------------------------------------------------------
// <copyright file="NamespaceStructure.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace SoulCenterProject.Modules.Ai.Model
{
    public class NamespaceStructure
    {
        public List<ClassStructure> Classes { get; set; }

        public string Name { get; set; }
    }
}
