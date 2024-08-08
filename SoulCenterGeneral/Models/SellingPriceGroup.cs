//-----------------------------------------------------------------------
// <copyright file="SellingPriceGroup.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SoulCenterProject.Models
{
    public class SellingPriceGroup
    {
        public DateTime created_at { get; set; }

        public int id { get; set; }

        public int price_group_id { get; set; }

        public string price_inc_tax { get; set; }

        public DateTime updated_at { get; set; }

        public int variation_id { get; set; }
    }
}
