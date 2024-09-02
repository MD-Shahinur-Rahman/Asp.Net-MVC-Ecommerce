using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.Models.ViewModels
{
    public class HomePageFeatureViewModel
    {
        public List<ProductCategory> Categories { get; set; }
        public List<ProductCategory> FeaturedCategories { get; set; }
        public List<Product> LatestProducts { get; set; }
        public List<Product> DiscountedProducts { get; set; }
        public List<Product> Products { get; set; }
    }
}