using System;

namespace ApplicationDev.Models
{
    public class OrderDetail
    {
        
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateAt { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}