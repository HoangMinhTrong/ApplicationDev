using System;

namespace ApplicationDev.Models
{
    public class ShoppingSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
    }
}