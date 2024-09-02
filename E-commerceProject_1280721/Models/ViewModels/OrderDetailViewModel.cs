using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Add this property
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
        public int OrderId { get; set; } // Assuming OrderId is added here as well
    }
}