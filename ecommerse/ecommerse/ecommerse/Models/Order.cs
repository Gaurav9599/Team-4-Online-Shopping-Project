using System;
using System.Collections.Generic;

namespace ecommerse.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? TotalPrice { get; set; }
        public int? CustOrderId { get; set; }

        public virtual CustomerOrder? CustOrder { get; set; }
    }
}
