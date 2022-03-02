using System;
using System.Collections.Generic;

namespace ecommerse.Models
{
    public partial class ShippingAddress
    {
        public int AddressId { get; set; }
        public string City { get; set; } = null!;
        public string? Village { get; set; }
        public string? Landmark { get; set; }
        public int Pincode { get; set; }
        public int? UserId { get; set; }

        public virtual CustomerDetail? User { get; set; }
    }
}
