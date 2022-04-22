using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationDev.Models
{
    public class ProductInStore
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public ICollection<Product> Products;
        [NotMapped]
        public  IEnumerable<SelectListItem> StoreList { get; set; }
    }
}