using System;

namespace JonasHendrickx.Shop.Models.Entities
{
    public class BasketLineItem
    {
        public Guid Id { get; set; }
        public uint Amount { get; set; }
        public Guid BasketId { get; set; }
        public virtual Basket Basket { get; set; }
        public Guid ProductListingId { get; set; }
        public virtual ProductListing ProductListing { get; set; }
    }
}