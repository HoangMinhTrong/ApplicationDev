﻿using System;
using System.ComponentModel.DataAnnotations;


namespace ApplicationDev.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeleteAt { get; set; }
    }
}