//-----------------------------------------------------------------------
// <copyright file="ProductTax.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    public class ProductTax
    {
        public decimal amount { get; set; }

        public int business_id { get; set; }

        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        public object deleted_at { get; set; }  // Could be DateTime?

        public int id { get; set; }

        public int is_tax_group { get; set; }

        public string name { get; set; }

        public DateTime updated_at { get; set; }

        public object woocommerce_tax_rate_id { get; set; }  // Unsure of type
    }
}
