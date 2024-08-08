//-----------------------------------------------------------------------
// <copyright file="VariationLocationDetail.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    public class VariationLocationDetail
    {
        public DateTime created_at { get; set; }

        public int id { get; set; }

        public int location_id { get; set; }

        public int product_id { get; set; }

        public int product_variation_id { get; set; }

        public string qty_available { get; set; }

        public DateTime updated_at { get; set; }

        public int variation_id { get; set; }
    }
}
