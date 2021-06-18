using System;

namespace JonasHendrickx.Shop.Api.Contracts.Basket
{
    public class AddProductListingInputModel
    {
        public Guid ProductListingId { get; set; }
        public uint Amount { get; set; }
    }
}
