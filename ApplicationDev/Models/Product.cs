using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationDev.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int InventoryId { get; set; }
        public int StoreId { get; set; }
        public int DiscountId { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public Store Store { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductDiscount ProductDiscount { get; set; }
        public ProductInventory ProductInventory { get; set; }
    }
}