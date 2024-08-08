//-----------------------------------------------------------------------
// <copyright file="ProductVariation.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SoulCenterProject.Models
{
    public class ProductVariation
    {
        public DateTime created_at { get; set; }

        public int id { get; set; }

        public int is_dummy { get; set; }

        public string name { get; set; }

        public int product_id { get; set; }

        public DateTime updated_at { get; set; }

        public object variation_template_id { get; set; } // Unsure of type

        public List<Variation> Variations { get; set; }
    }
}
