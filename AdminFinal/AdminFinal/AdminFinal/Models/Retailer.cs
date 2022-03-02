using System;
using System.Collections.Generic;

namespace AdminFinal.Models
{
    public partial class Retailer
    {
        public Retailer()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int RetailerId { get; set; }
        public string RetailerName { get; set; } = null!;
        public string RetailerEmail { get; set; } = null!;
        public string? RetailerPhoneNum { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string City { get; set; } = null!;
        public int Pincode { get; set; }
        public string Password { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public bool? ApprovedStatus { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
