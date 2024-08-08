//-----------------------------------------------------------------------
// <copyright file="Unit.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    public class Unit
    {
        public string actual_name { get; set; }

        public int allow_decimal { get; set; }

        public object base_unit_id { get; set; } // Unsure of type

        public object base_unit_multiplier { get; set; } // Unsure of type

        public int business_id { get; set; }

        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public object deleted_at { get; set; } // Could be DateTime?

        public int id { get; set; }

        public string short_name { get; set; }

        public DateTime updated_at { get; set; }
    }
}
