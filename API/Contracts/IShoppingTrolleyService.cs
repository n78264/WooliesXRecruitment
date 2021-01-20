using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts
{
    public interface IShoppingTrolleyService
    {
        decimal GetLowestTrolleyTotal(ShoppingTrolley request);
    }
}
