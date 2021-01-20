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
    public class WooliesXService : IWooliesXService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<ResourceSettings> _config;
        private readonly ILogger<ProductService> _logger;

        public WooliesXService(IHttpClientFactory httpClientFactory,
            IOptions<ResourceSettings> config, ILogger<ProductService> logger)
        {
            _httpClientFactory = httpClientFactory ??
                    throw new ArgumentNullException(nameof(httpClientFactory));
            _config = config;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public Task<IEnumerable<Product>> GetProducts()
        {
            return GetHttpResource<IEnumerable<Product>>(_httpClientFactory,
                        _config, "products");
        }

        public Task<IEnumerable<ShopperHistory>> GetShopperHistory()
        {
            return GetHttpResource<IEnumerable<ShopperHistory>>(
                                _httpClientFactory, _config, "shopperHistory");

        }

        private async Task<T> GetHttpResource<T>(IHttpClientFactory httpClientFactory,
                IOptions<ResourceSettings> _config, string resourceName)
        {
            var url = QueryHelpers.AddQueryString($"{_config.Value.BaseUrl}/" +
                        $"{_config.Value.RelativePath}/{resourceName}",
                        new Dictionary<string, string> { { "token", _config.Value.Token } });

            T data;
            var client = httpClientFactory.CreateClient();
            try
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return default(T);
        }


    }
}
