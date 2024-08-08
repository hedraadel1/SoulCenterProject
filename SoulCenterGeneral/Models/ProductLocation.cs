//-----------------------------------------------------------------------
// <copyright file="ProductLocation.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SoulCenterProject.Models
{
    public class ProductLocation
    {
        public object alternate_number { get; set; } // Unsure of type

        public int business_id { get; set; }

        public string city { get; set; }

        public string country { get; set; }

        public DateTime created_at { get; set; }

        public object custom_field1 { get; set; } // Unsure of type

        public object custom_field2 { get; set; } // Unsure of type

        public object custom_field3 { get; set; } // Unsure of type

        public object custom_field4 { get; set; } // Unsure of type

        public string default_payment_accounts { get; set; } // Complex JSON structure, may need a custom class

        public object deleted_at { get; set; } // Possible DateTime?

        public object email { get; set; } // Unsure of type

        public List<string> featured_products { get; set; } // Assuming a list of string IDs

        public int id { get; set; }

        public int invoice_layout_id { get; set; }

        public int invoice_scheme_id { get; set; }

        public int is_active { get; set; }

        public string landmark { get; set; }

        public object location_id { get; set; } // Unsure of type

        public object mobile { get; set; } // Unsure of type

        public string name { get; set; }

        public Pivot pivot { get; set; }

        public int print_receipt_on_invoice { get; set; }

        public object printer_id { get; set; } // Unsure of type

        public string receipt_printer_type { get; set; }

        public object selling_price_group_id { get; set; } // Unsure of type

        public string state { get; set; }

        public DateTime updated_at { get; set; }

        public object website { get; set; } // Unsure of type

        public string zip_code { get; set; }
    }

    public class Pivot
    {
        public int location_id { get; set; }

        public int product_id { get; set; }
    }
}
