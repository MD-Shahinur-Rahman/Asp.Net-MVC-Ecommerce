using E_commerceProject_1280721.DAL;
using E_commerceProject_1280721.Models.ViewModels;
using E_commerceProject_1280721.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace E_commerceProject_1280721.Controllers
{
    public class OrderController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        [HttpGet]
        public ActionResult PlaceOrder()
        {
            var cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var orderViewModel = new OrderViewModel
            {
                OrderDetails = cartItems.Select(item => new OrderDetailViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Subtotal = item.Quantity * item.Price
                }).ToList()
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PlaceOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, re-populate order details and return to the view
                var cartItems = Session["CartItems"] as List<CartItem>;
                if (cartItems != null)
                {
                    foreach (var item in cartItems)
                    {
                        var product = await db.Products.FindAsync(item.ProductId); // Assume you have a Product entity
                        model.OrderDetails.Add(new OrderDetailViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = product.ProductName, // Assuming Product entity has a Name property
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Subtotal = item.Quantity * item.Price
                        });
                    }
                }

                return View(model); // Return to view to display validation errors
            }

            try
            {
                // Calculate shipping cost based on shipping area
                decimal shippingCost = model.ShippingArea == "InsideDhaka" ? 100 : 200;

                // Calculate subtotal and total payable
                decimal subtotal = model.OrderDetails.Sum(item => item.Quantity * item.Price);
                decimal totalPayable = subtotal + shippingCost;

                // Example: Assuming you're using ASP.NET Identity for user management
                string userId = User.Identity.GetUserId();

                // Create the order object
                var order = new Order
                {
                    UserId = userId,
                    Name = model.Name,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    ShippingAddress = model.ShippingAddress,
                    OrderNote = model.OrderNote,
                    TotalPayable = totalPayable,
                    ShippingArea = model.ShippingArea,
                    PaymentMethod = "CashOnDelivery", // Example: Assuming fixed payment method
                    OrderDetails = new List<OrderDetail>()
                };

                // Add OrderDetails to the Order
                foreach (var od in model.OrderDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Subtotal = od.Subtotal,
                        Order = order // Link to the order
                    };
                    order.OrderDetails.Add(orderDetail);
                }

                // Save the order and order details to the database
                db.Orders.Add(order);
                await db.SaveChangesAsync();

                // Clear the cart after placing the order
                Session["CartItems"] = new List<CartItem>();

                // Redirect to the order confirmation page
                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "An error occurred while placing the order. Please try again.");
                return View(model);
            }
        }








        public async Task<ActionResult> OrderConfirmation(int orderId)
        {
            var order = await db.Orders
            .Include(o => o.OrderDetails.Select(od => od.Product))
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null || order.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            var viewModel = new OrderConfirmationViewModel
            {
                OrderId = order.OrderId,
                Name = order.Name,
                Email = order.Email,
                Mobile = order.Mobile,
                ShippingAddress = order.ShippingAddress,
                OrderNote = order.OrderNote,
                TotalPayable = order.TotalPayable,
                ShippingArea = order.ShippingArea,
                PaymentMethod = order.PaymentMethod,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    ProductName = od.Product.ProductName,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Subtotal = od.Subtotal
                }).ToList()
            };

            return View(viewModel);
        }
    }
}