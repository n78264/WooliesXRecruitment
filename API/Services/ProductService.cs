using API.Contracts;
using API.Extensions;
using API.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IWooliesXService _wooliesXService;
        public ProductService(IWooliesXService wooliesXService)
        {
            _wooliesXService = wooliesXService;
        }

        public async Task<IEnumerable<Product>> GetProducts(SortOption sortOption)
        {
            var currentProductList = await _wooliesXService.GetProducts();
            var historicalProductsList = await _wooliesXService.GetShopperHistory();

            if (sortOption == SortOption.Recommended)
            {
                foreach (var x in historicalProductsList.SelectMany(x => x.Products)
                               .GroupBy(pv => pv.Name)
                               .Select(g => new ProductSalesHistory
                               {
                                   Name = g.Key,
                                   TotalQuantity = g.Sum(x => x.Quantity),
                                   TotalProductSold = g.Count()
                               }))
                {
                    var popularityRankToUpdate = currentProductList.FirstOrDefault(d => d.Name.Trim().ToLower() == x.Name.Trim().ToLower());
                    if (popularityRankToUpdate != null)
                        popularityRankToUpdate.PopularityRank = x.TotalProductSold;
                }
                return currentProductList.OrderByDescending(x => x.PopularityRank);
            }
            else
                return currentProductList.Sort(sortOption);
        }
    }
}
