using E_commerceProject_1280721.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
    }
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}