using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JonasHendrickx.Shop.Api.Contracts.Basket;
using JonasHendrickx.Shop.Api.Contracts.Discount;
using JonasHendrickx.Shop.Api.Contracts.Product;
using JonasHendrickx.Shop.Api.Contracts.ProductListing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JonasHendrickx.Shop.Api.IntegrationTests.Controllers
{
    public class BaseControllerIntegrationTests
    {
        
        private IHost _host;
        protected HttpClient Client;

        [SetUp]
        public async Task SetUp()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                });
            _host = await hostBuilder.StartAsync();
            Client = _host.GetTestClient();
        }
        
        #region BasketController
        /// <summary>
        /// 
        /// </summary>
        /// <returns>BasketId</returns>
        protected async Task<Guid> CreateBasketAsync()
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(string.Empty);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync("/Basket", byteContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var basketId = JsonConvert.DeserializeObject<Guid>(jsonResponse);
            return basketId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basketId"></param>
        /// <param name="productListingId"></param>
        /// <param name="amount"></param>
        /// <returns>BasketLineItemId</returns>
        protected async Task<Guid> AddProductListingToBasketAsync(Guid basketId, Guid productListingId, uint amount)
        {
            var inputModel = new AddProductListingInputModel
            {
                Amount = amount,
                ProductListingId = productListingId
            };
            var json = JsonConvert.SerializeObject(inputModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync($"/Basket/{basketId}/AddProductListing", byteContent);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var basketLineItemId = JsonConvert.DeserializeObject<Guid>(jsonResponse);
            return basketLineItemId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basketId">BasketId</param>
        /// <returns>Value of basket</returns>
        protected async Task<decimal> GetBasketAmountAsync(Guid basketId)
        {
            var response = await Client.GetAsync($"/Basket/{basketId}/Amount");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var price = JsonConvert.DeserializeObject<decimal>(jsonResponse);
            return price;
        }
        
        protected async Task DeleteBasketAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"/Basket/{id}");
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception("Do something");
            }
        }
        #endregion
        
        #region ProductController
        /// <summary>
        /// 
        /// </summary>
        /// <returns>ProductId</returns>
        protected async Task<Guid> CreateProductAsync(string code, string name)
        {
            var inputModel = new CreateProductInputModel
            {
                Code = code,
                Name = name
            };
            var json = JsonConvert.SerializeObject(inputModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync("/Product", byteContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var productId = JsonConvert.DeserializeObject<Guid>(jsonResponse);
            return productId;
        }
        #endregion
        
        #region ProductListingController
        /// <summary>
        /// 
        /// </summary>
        /// <returns>ProductListingId</returns>
        protected async Task<Guid> CreateProductListingAsync(decimal price, Guid productId, DateTime startDate, DateTime? endDate = null)
        {
            var inputModel = new CreateProductListingInputModel
            {
                Price = price,
                StartDate = startDate,
                EndDate = endDate,
                ProductId = productId
            };
            var json = JsonConvert.SerializeObject(inputModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync("/ProductListing", byteContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var productListingId = JsonConvert.DeserializeObject<Guid>(jsonResponse);
            return productListingId;
        }
        #endregion
        
        #region DiscountController
        /// <summary>
        /// 
        /// </summary>
        /// <returns>DiscountId</returns>
        protected async Task<Guid> CreateDiscountAsync(string code, string rules, Guid productListingId)
        {
            var inputModel = new CreateDiscountInputModel
            {
                Code = code,
                ProductListingId = productListingId,
                Rules = rules
            };
            var json = JsonConvert.SerializeObject(inputModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync("/Discount", byteContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var discountId = JsonConvert.DeserializeObject<Guid>(jsonResponse);
            return discountId;
        }
        #endregion
    }
}