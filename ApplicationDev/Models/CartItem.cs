using System;

namespace ApplicationDev.Models
{
    public struct CartItem
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreateAt { get; set; }
    }
}