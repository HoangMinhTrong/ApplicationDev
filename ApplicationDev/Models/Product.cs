﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Models
{
    public class Product
    {
        [Key]
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }
        public string ImgUrl { get; set; }
        public int StoreId { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Store Store { get; set; }
        [NotMapped]
        public  IEnumerable<SelectListItem> ProductCategoryList { get; set; }
        [NotMapped]
        public  IEnumerable<SelectListItem> StoreList { get; set; }
    }
}