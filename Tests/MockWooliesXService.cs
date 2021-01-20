using API.Contracts;
using API.Extensions;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class MockWooliesXService : IWooliesXService
    {
        public const string ProductNameA = "Test Product A";
        public const string ProductNameB = "Test Product B";
        public const string ProductNameC = "Test Product C";
        public const string ProductNameD = "Test Product D";
        public const string ProductNameE = "Test Product E";
        public const string ProductNameF = "Test Product F";

        public async Task<IEnumerable<Product>> GetProducts() => new List<Product>()
            {
                new Product { Name = ProductNameA, Price = 99.99M, Quantity = 0 },
                new Product { Name = ProductNameB, Price = 101.99M, Quantity = 0 },
                new Product { Name = ProductNameC, Price = 10.99M, Quantity = 0 },
                new Product { Name = ProductNameD, Price = 5M, Quantity = 0 },
                new Product { Name = ProductNameF, Price = 999999999999M, Quantity = 0 }
            };

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistory() => new List<ShopperHistory>()
            {
                new ShopperHistory
                {
                    CustomerId = 123,
                    Products = new List<Product> ()
                    {
                        new Product { Name = ProductNameA, Price = 99.99M, Quantity = 3 },
                        new Product { Name = ProductNameB, Price = 101.99M, Quantity = 1 },
                        new Product { Name = ProductNameF, Price = 999999999999M, Quantity = 1 }
                    }
                },
                new ShopperHistory
                {
                    CustomerId = 23,
                    Products = new List<Product> ()
                    {
                        new Product { Name = ProductNameA, Price = 99.99M, Quantity = 2 },
                        new Product { Name = ProductNameB, Price = 101.99M, Quantity = 3 },
                        new Product { Name = ProductNameF, Price = 999999999999M, Quantity = 1 }
                    }
                },
                new ShopperHistory
                {
                    CustomerId = 23,
                    Products = new List<Product> ()
                    {
                        new Product { Name = ProductNameC, Price = 10.99M, Quantity = 2 },
                        new Product { Name = ProductNameF, Price = 999999999999M, Quantity = 2 }
                    }
                },
                new ShopperHistory
                {
                    CustomerId = 23,
                    Products = new List<Product> ()
                    {
                        new Product { Name = ProductNameA, Price = 99.99M, Quantity = 1 },
                        new Product { Name = ProductNameB, Price = 101.99M, Quantity = 1 },
                        new Product { Name = ProductNameC, Price = 10.99M, Quantity = 1 }
                    }
                },
            };
    }
}
