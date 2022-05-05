using System;
using System.Collections.Generic;

namespace ApplicationDev.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Paid { get; set; }
        public string Note { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}