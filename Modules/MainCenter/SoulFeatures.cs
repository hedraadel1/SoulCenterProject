//-----------------------------------------------------------------------
// <copyright file="SoulFeatures.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using Microsoft.Build.Framework;

namespace SoulCenterProject.Modules.MainCenter
{
    public partial class SoulFeatures : DevExpress.XtraEditors.XtraUserControl
    {
        public SoulFeatures()
        {
            InitializeComponent();

            BindingList<Customer> dataSource = GetDataSource();
            gridControl.DataSource = dataSource;
            bsiRecordsCount.Caption = "RECORDS : " + dataSource.Count;
        }

        void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e) { gridControl.ShowRibbonPrintPreview(); }

        public BindingList<Customer> GetDataSource()
        {
            BindingList<Customer> result = new BindingList<Customer>();
            result.Add(
                new Customer()
                {
                    ID = 1,
                    Name = "ACME",
                    Address = "2525 E El Segundo Blvd",
                    City = "El Segundo",
                    State = "CA",
                    ZipCode = "90245",
                    Phone = "(310) 536-0611"
                });
            result.Add(
                new Customer()
                {
                    ID = 2,
                    Name = "Electronics Depot",
                    Address = "2455 Paces Ferry Road NW",
                    City = "Atlanta",
                    State = "GA",
                    ZipCode = "30339",
                    Phone = "(800) 595-3232"
                });
            return result;
        }

        public class Customer
        {
            public string Address { get; set; }

            public string City { get; set; }

            [Key, Display(AutoGenerateField = false)]
            public int ID { get; set; }

            [Required]
            public string Name { get; set; }

            public string Phone { get; set; }

            public string State { get; set; }

            [Display(Name = "Zip Code")]
            public string ZipCode { get; set; }
        }
    }

    public class DisplayAttribute : Attribute
    {
        public bool AutoGenerateField
        {
            get { return false; }
            set
            {
            }
        }

        public string Name
        {
            get { return null; }
            set
            {
            }
        }
    }
}
