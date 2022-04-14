using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationDev.Models
{
    public class ProductInventory
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public Product Product { get; set; }
    }
}