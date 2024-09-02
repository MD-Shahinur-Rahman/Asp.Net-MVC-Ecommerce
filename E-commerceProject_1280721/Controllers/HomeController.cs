using E_commerceProject_1280721.DAL;
using E_commerceProject_1280721.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using E_commerceProject_1280721.Models;
using System.Data.Entity;

namespace E_commerceProject_1280721.Controllers
{
    public class HomeController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        public ActionResult Index()
        {
            var categories = db.ProductCategories.ToList();


            var featuredCategories = (from c in db.ProductCategories
                                      where c.CategoryName == "HP" ||
                                            c.CategoryName == "Apple" ||
                                            c.CategoryName == "Lenevo" ||
                                            c.CategoryName == "Asus"
                                      select c).ToList();

            foreach (var category in featuredCategories)
            {
                db.Entry(category).Collection(c => c.Products).Load();
            }

            var discountedProducts = (from p in db.Products
                                      where p.DiscountId != null
                                      orderby p.ProductId descending
                                      select p).Take(10).ToList();

            foreach (var product in discountedProducts)
            {
                db.Entry(product).Reference(p => p.Discount).Load();
            }

            var latestProducts = db.Products
                              .OrderByDescending(p => p.ProductId)
                              .Take(10)
                              .ToList();

            var viewModel = new HomePageFeatureViewModel
            {
                Categories = categories,
                FeaturedCategories = featuredCategories,
                DiscountedProducts = discountedProducts,
                LatestProducts = latestProducts
            };

            return View(viewModel);
        }

        public async Task<ActionResult> Shop(int? page)
        {
            int pageSize = 12;

            var productsQuery = db.Products.OrderByDescending(p => p.ProductId);

            var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, page ?? 1, pageSize);


            return View(paginatedProducts);
        }

        public async Task<ActionResult> LiveSearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Json(new List<Product>(), JsonRequestBehavior.AllowGet);
            }

            var products = await db.Products
                .Where(p => p.ProductName.Contains(searchTerm))
                .Select(p => new {
                    p.ProductId,
                    p.ProductName,
                    p.ImageUrl
                })
                .ToListAsync();



            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ProductDetails(int id)
        {
            var product = await db.Products
                                 .Include(p => p.ProductCategory)
                                 .Include(p => p.ProductCategory.ProductSubCategories)
                                 .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }



    }
}