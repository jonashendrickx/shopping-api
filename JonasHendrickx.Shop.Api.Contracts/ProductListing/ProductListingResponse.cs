using System;

namespace JonasHendrickx.Shop.Api.Contracts.ProductListing
{
    public class ProductListingResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}