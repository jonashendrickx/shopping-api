using System;
using System.Collections.Generic;

namespace JonasHendrickx.Shop.Models.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public virtual ICollection<BasketLineItem> LineItems { get; set; }
    }
}