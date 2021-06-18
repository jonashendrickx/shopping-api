using System;

namespace JonasHendrickx.Shop.Api.Contracts.ProductListing
{
    public class CreateProductListingInputModel
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}