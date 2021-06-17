using System;

namespace JonasHendrickx.Shop.Models.Entities
{
    public class Discount
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid ProductListingId { get; set; }
        public virtual ProductListing ProductListing { get; set; }
        public string Rules { get; set; }
    }
}