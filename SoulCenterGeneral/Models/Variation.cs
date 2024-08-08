//-----------------------------------------------------------------------
// <copyright file="Variation.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SoulCenterProject.Models
{
    public class Variation
    {
        public object combo_variations { get; set; } // Need more info on structure

        public DateTime created_at { get; set; }

        public string default_purchase_price { get; set; }

        public string default_sell_price { get; set; }

        public object deleted_at { get; set; } // Could be DateTime?

        public string dpp_inc_tax { get; set; }

        public int id { get; set; }

        public List<Media> media { get; set; }

        public string name { get; set; }

        public int product_id { get; set; }

        public int product_variation_id { get; set; }

        public string profit_percent { get; set; }

        public string sell_price_inc_tax { get; set; }

        public List<SellingPriceGroup> selling_price_group { get; set; }

        public string sub_sku { get; set; }

        public DateTime updated_at { get; set; }

        public List<VariationLocationDetail> variation_location_details { get; set; }

        public object variation_value_id { get; set; } // Unsure of type

        public object woocommerce_variation_id { get; set; } // Unsure of type
    }
}
