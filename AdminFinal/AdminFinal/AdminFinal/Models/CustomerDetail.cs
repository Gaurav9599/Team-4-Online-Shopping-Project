using System;
using System.Collections.Generic;

namespace AdminFinal.Models
{
    public partial class CustomerDetail
    {
        public CustomerDetail()
        {
            Carts = new HashSet<Cart>();
            CustomerOrders = new HashSet<CustomerOrder>();
            ShippingAddresses = new HashSet<ShippingAddress>();
        }

        public int CustId { get; set; }
        public string CustName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNum { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
    }
}
