using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Contracts
{
    public interface IWooliesXService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<ShopperHistory>> GetShopperHistory();
    }
}
