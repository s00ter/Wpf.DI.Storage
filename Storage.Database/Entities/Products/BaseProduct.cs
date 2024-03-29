﻿using Storage.Database.Enums;

namespace Storage.Database.Entities.Products
{
    public class BaseProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Cost { get; set; }
        public string Coming { get; set; }
        public int Amount { get; set; }
        public string VendorCode { get; set; }
        public ProductStatus Status { get; set; }
        public string Info { get; set; }
        public string Provider { get; set; }
        public DimensionType DimensionType { get; set; }
        public ProductType ProductType { get; set; }

        public ProductOwner ProductOwner { get; set; }
    }
}