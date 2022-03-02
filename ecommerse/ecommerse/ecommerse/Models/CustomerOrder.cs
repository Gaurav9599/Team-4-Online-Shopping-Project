using System;
using System.Collections.Generic;

namespace ecommerse.Models
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            Orders = new HashSet<Order>();
        }

        public int CustOrderId { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? PurchasingQuantity { get; set; }
        public int? TotalPrice { get; set; }
        public DateTime Orderdate { get; set; }

        public virtual CustomerDetail? Customer { get; set; }
        public virtual ProductDetail? Product { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
