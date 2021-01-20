using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductSalesHistory
    {
        public string Name { get; set; }
        public long TotalQuantity { get; set; }
        public int TotalProductSold { get; set; }
    }
}
