//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace SoulCenterProject.Models
{
    public class Product
    {
        public string alert_quantity { get; set; }

        public string barcode_type { get; set; }

        public Brand brand { get; set; }

        public int business_id { get; set; }

        public Category category { get; set; }

        public int created_by { get; set; }

        public int ecom_active_in_store { get; set; }

        public object ecom_shipping_class_id { get; set; } // Unsure of type

        public int enable_sr_no { get; set; }

        public int enable_stock { get; set; }

        public object expiry_period { get; set; } // Unsure of type

        public object expiry_period_type { get; set; } // Unsure of type

        public int id { get; set; }

        public string image { get; set; }

        public string image_url { get; set; }

        public int is_inactive { get; set; }

        public string name { get; set; }

        public int not_for_selling { get; set; }

        public string product_custom_field1 { get; set; }

        public string product_custom_field2 { get; set; }

        public string product_custom_field3 { get; set; }

        public string product_custom_field4 { get; set; }

        public string product_description { get; set; }

        public List<ProductLocation> product_locations { get; set; }

        public ProductTax product_tax { get; set; }

        public List<ProductVariation> product_variations { get; set; }

        public object repair_model_id { get; set; } // Unsure of type

        public string sku { get; set; }

        public SubCategory sub_category { get; set; }

        public List<int> sub_unit_ids { get; set; } // Assuming an array of integers here

        public string type { get; set; }

        public Unit unit { get; set; }

        public object warranty_id { get; set; } // Unsure of type

        public object weight { get; set; } // Unsure of type

        public int woocommerce_disable_sync { get; set; }

        public object woocommerce_media_id { get; set; } // Unsure of type

        public int? woocommerce_product_id { get; set; }
    }
}
