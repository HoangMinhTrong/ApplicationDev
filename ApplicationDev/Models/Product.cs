using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationDev.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeleteAt { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Store Store { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [NotMapped]
        public  IEnumerable<SelectListItem> ProductCategoryList { get; set; }
        [NotMapped]
        public  IEnumerable<SelectListItem> StoreList { get; set; }
    }
}