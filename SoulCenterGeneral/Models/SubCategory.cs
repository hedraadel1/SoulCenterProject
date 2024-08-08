//-----------------------------------------------------------------------
// <copyright file="SubCategory.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    public class SubCategory
    {
        public int business_id { get; set; }

        public string category_type { get; set; }

        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public object deleted_at { get; set; }  // Could be DateTime?

        public object description { get; set; }  // Unsure of type

        public int id { get; set; }

        public string name { get; set; }

        public int parent_id { get; set; }

        public object short_code { get; set; } // Unsure of type

        public object slug { get; set; }  // Unsure of type

        public DateTime updated_at { get; set; }

        public object woocommerce_cat_id { get; set; }  // Unsure of type
    }
}
