using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_commerceProject_1280721.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        [Key]
        public int ProductCategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Image")]
        public string CategoryImage { get; set; }

        [Display(Name = "Upload Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}