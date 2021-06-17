using System;
using System.Collections.Generic;

namespace JonasHendrickx.Shop.Models.Entities
{
    public class ProductListing
    {
        public Guid Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<BasketLineItem> BasketLineItems { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
    }
}