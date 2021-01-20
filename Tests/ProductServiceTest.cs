using API.Contracts;
using API.Models;
using API.Services;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    [TestClass]
    public class ProductServiceTest
    {
        private IProductService _productService;
        private Mock<IProductService> _mockedIProductService;
        private List<Product> _products;

        private const string productAName = "testA";
        private const string productBName = "testB";
        private const string productCName = "testC";

        [TestInitialize()]
        public void Initialize()
        {
            //var configuration = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json", false).Build();
            //var options = Options.Create(configuration.GetSection("WooliesXAPISettings").
            //                 Get<ResourceSettings>());


            //var optionsMock = new Mock<IOptions<ResourceSettings>>();
            //var loggerMock = new Mock<ILogger<ProductService>>();
            //var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            //_productService = new ProductService(httpClientFactoryMock.Object, options,
            //                            loggerMock.Object);
        }

        [TestMethod]
        public void TestGetProducts()
        {
            ProductService service = new ProductService(new MockWooliesXService());
            this.TestProductNamesOrder(service.GetProducts(SortOption.Ascending).GetAwaiter().GetResult().ToList(), MockWooliesXService.ProductNameA, MockWooliesXService.ProductNameF);
            this.TestProductNamesOrder(service.GetProducts(SortOption.Descending).GetAwaiter().GetResult().ToList(), MockWooliesXService.ProductNameF, MockWooliesXService.ProductNameA);
            this.TestProductNamesOrder(service.GetProducts(SortOption.Recommended).GetAwaiter().GetResult().ToList(), MockWooliesXService.ProductNameA, MockWooliesXService.ProductNameD);
            this.TestProductNamesOrder(service.GetProducts(SortOption.High).GetAwaiter().GetResult().ToList(), MockWooliesXService.ProductNameF, MockWooliesXService.ProductNameD);
            this.TestProductNamesOrder(service.GetProducts(SortOption.Low).GetAwaiter().GetResult().ToList(), MockWooliesXService.ProductNameD, MockWooliesXService.ProductNameF);
        }

        private void TestProductNamesOrder(List<Product> products, string firstExpected, string lastExpected)
        {
            Assert.AreEqual(products[0].Name, firstExpected);
            Assert.AreEqual(products[products.Count - 1].Name, lastExpected);
        }
        [TestCleanup()]
        public void Cleanup() { }


    }
}
