﻿using E_commerceProject_1280721.DAL;
using E_commerceProject_1280721.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_commerceProject_1280721.Controllers
{
    public class CartController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        [HttpPost]
        public ActionResult AddToCart(int productId)
        {
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cartItems = Session["CartItems"] as List<CartItem> ?? new List<CartItem>();

            var cartItem = cartItems.SingleOrDefault(item => item.ProductId == productId);
            if (cartItem == null)
            {
                // Generate a new CartItemId
                int newCartItemId = cartItems.Any() ? cartItems.Max(item => item.CartItemId) + 1 : 1;

                cartItems.Add(new CartItem
                {
                    CartItemId = newCartItemId,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = 1,
                    ProductImage = product.ImageUrl
                });
            }
            else
            {
                cartItem.Quantity++;
            }

            Session["CartItems"] = cartItems;

            return Json(new
            {
                success = true,
                totalItemCount = cartItems.Sum(item => item.Quantity)
            });
        }



        [HttpPost]
        public ActionResult RemoveFromCart(int cartItemId)
        {
            var cartItems = Session["CartItems"] as List<CartItem> ?? new List<CartItem>();
            var cartItem = cartItems.SingleOrDefault(item => item.CartItemId == cartItemId);

            if (cartItem != null)
            {
                cartItems.Remove(cartItem);
            }

            Session["CartItems"] = cartItems;

            return Json(new
            {
                success = true,
                totalItemCount = cartItems.Sum(item => item.Quantity),
                totalPrice = cartItems.Sum(item => item.Price * item.Quantity)
            });
        }

        [HttpPost]
        public ActionResult UpdateCartItem(int cartItemId, int quantity)
        {
            var cartItems = Session["CartItems"] as List<CartItem> ?? new List<CartItem>();
            var cartItem = cartItems.SingleOrDefault(item => item.CartItemId == cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                Session["CartItems"] = cartItems;

                return Json(new
                {
                    success = true,
                    itemPrice = cartItem.Price,
                    subtotal = cartItems.Sum(item => item.Price * item.Quantity),
                    total = cartItems.Sum(item => item.Price * item.Quantity)
                });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public JsonResult GetCartTotal()
        {
            var cartItems = Session["CartItems"] as List<CartItem> ?? new List<CartItem>();

            decimal subtotal = cartItems.Sum(item => item.Price * item.Quantity);
            decimal total = subtotal;

            return Json(new { subtotal = subtotal, total = total }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCartSummary()
        {
            var cartItems = Session["CartItems"] as List<CartItem> ?? new List<CartItem>();

            int itemCount = cartItems.Sum(item => item.Quantity);
            decimal totalPrice = cartItems.Sum(item => item.Price * item.Quantity);

            return Json(new { itemCount, totalPrice }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var cartItems = Session["CartItems"] as List<CartItem> ?? new List<CartItem>();
            return View(cartItems);
        }
    }
}