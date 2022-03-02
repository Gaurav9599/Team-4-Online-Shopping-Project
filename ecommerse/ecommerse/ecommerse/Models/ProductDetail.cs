using System;
using System.Collections.Generic;

namespace ecommerse.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            Carts = new HashSet<Cart>();
            CustomerOrders = new HashSet<CustomerOrder>();
        }

        public int ProductId { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductName { get; set; }
        public string? Fabric { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }
        public double? Ratings { get; set; }
        public string? Images { get; set; }
        public int? RetailerId { get; set; }

        public virtual Retailer? Retailer { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
