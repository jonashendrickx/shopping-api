using System;

namespace JonasHendrickx.Shop.Api.Contracts
{
    public class AddProductListingInputModel
    {
        public Guid ProductListingId { get; set; }
        public uint Amount { get; set; }
    }
}
