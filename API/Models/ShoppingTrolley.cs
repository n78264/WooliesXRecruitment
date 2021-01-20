using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ShoppingTrolley
    {
        public IEnumerable<TrolleyProduct> Products { get; set; }
        public IEnumerable<ProductQuantity> Quantities { get; set; }
        public IEnumerable<Special> Specials { get; set; }
    }

    public class TrolleyProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductQuantity
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }

    public class Special
    {
        public List<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}
