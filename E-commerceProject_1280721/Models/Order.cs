using E_commerceProject_1280721.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderNote { get; set; }

        public decimal TotalPayable { get; set; }
        public string ShippingArea { get; set; }
        public string PaymentMethod { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderCancel> OrderCancels { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}