using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.Models.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderNote { get; set; }
        public decimal TotalPayable { get; set; }
        public string ShippingArea { get; set; }
        public string PaymentMethod { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}