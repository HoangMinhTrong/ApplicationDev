using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationDev.Models
{
    public class ProductDiscount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime DeleteAt { get; set; }
    }
}