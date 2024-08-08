//-----------------------------------------------------------------------
// <copyright file="Brand.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using SoulCenterProject.Helpers.Utils;

namespace SoulCenterProject.Models
{
    [ClassInfo("Model")]
    public class Brand
    {
        public int business_id { get; set; }

        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public object deleted_at { get; set; } // Could be DateTime?

        public object description { get; set; } // Unsure of type

        public int id { get; set; }

        public string name { get; set; }

        public DateTime updated_at { get; set; }
    }
}
