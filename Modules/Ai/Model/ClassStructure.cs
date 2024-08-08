//-----------------------------------------------------------------------
// <copyright file="ClassStructure.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace SoulCenterProject.Modules.Ai.Model
{
    public class ClassStructure
    {
        public List<MethodStructure> Methods { get; set; }

        public string Name { get; set; }
    }
}
