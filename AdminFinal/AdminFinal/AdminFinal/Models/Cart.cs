using System;
using System.Collections.Generic;

namespace AdminFinal.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public int? TotalPrice { get; set; }

        public virtual CustomerDetail? Customer { get; set; }
        public virtual ProductDetail? Product { get; set; }
    }
}
