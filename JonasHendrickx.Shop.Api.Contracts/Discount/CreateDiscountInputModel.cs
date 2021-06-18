using System;

namespace JonasHendrickx.Shop.Api.Contracts.Discount
{
    public class CreateDiscountInputModel
    {
        public string Code { get; set; }
        public string Rules { get; set; }
        public Guid ProductListingId { get; set; }
    }
}