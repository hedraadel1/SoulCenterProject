//-----------------------------------------------------------------------
// <copyright file="Media.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using SoulCenterProject.Helpers.Utils;

namespace SoulCenterProject.Models
{
    [ClassInfo("Model")]
    public class Media
    {
        public int business_id { get; set; }

        public DateTime created_at { get; set; }

        public object description { get; set; } // Unsure of type

        public string display_name { get; set; }

        public string display_url { get; set; }

        public string file_name { get; set; }

        public int id { get; set; }

        public int model_id { get; set; }

        public string model_type { get; set; }

        public DateTime updated_at { get; set; }

        public int uploaded_by { get; set; }

        public object woocommerce_media_id { get; set; } // Unsure of type
    }
}
